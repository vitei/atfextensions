using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sce.Atf.Input;
using Sce.Atf.Applications;
using Sce.Atf;
using Sce.Atf.Dom;
using Sce.Atf.Adaptation;

namespace Vitei.FMODInterop
{
    [Export(typeof(IInitializable))]
    [Export(typeof(Vitei.FMODInterop.SoundSystem))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SoundSystem : IInitializable
    {
        FMOD.System m_fmodSystem;
        FMOD.ChannelGroup m_masterChannelGroup;
        
        [ImportingConstructor]
        public SoundSystem()
        {
            FMOD.Factory.System_Create(out m_fmodSystem);
        }            

        public void Initialize()
        {
            m_fmodSystem.init(
                32,
                FMOD.INITFLAGS.NORMAL,
                IntPtr.Zero
            );
            m_fmodSystem.getMasterChannelGroup(out m_masterChannelGroup);
        }

        public void Teardown()
        {
            m_masterChannelGroup.release();
            m_fmodSystem.update();
            m_fmodSystem.close();
            m_fmodSystem.release();
        }

        public void StopAll()
        {
            FMOD.ChannelGroup cg;
            m_fmodSystem.getMasterChannelGroup(out cg);
            cg.stop();
        }

        Dictionary<int, FMOD.ChannelGroup> m_ChannelGroups = new Dictionary<int, FMOD.ChannelGroup>();

        /// <summary>
        /// Create a channel group given a particular id.
        /// </summary>
        /// <param name="p_id"></param>
        /// <returns></returns>
        public FMOD.ChannelGroup GetChannelGroup(int p_id)
        {
            FMOD.ChannelGroup cg;

            if (m_ChannelGroups.ContainsKey(p_id))
            {
                cg = m_ChannelGroups[p_id];
            } else
            {
                m_fmodSystem.createChannelGroup(
                    string.Format("channelgroup{0}", p_id),
                    out cg
                );
                m_ChannelGroups[p_id] = cg;
            }

            return cg;
        }

        /// <summary>
        /// Load and play a sound at a given location.
        /// </summary>
        /// <param name="p_filename">Path to a supported sound file.</param>
        /// <param name="p_bLooped">Whether or not to loop playback.</param>
        /// <returns></returns>
        public bool LoadSound(string p_filename, bool p_bLooped, out FMOD.Sound o_sound)
        {
            FMOD.RESULT r = m_fmodSystem.createSound(
                p_filename,
                p_bLooped? FMOD.MODE.LOOP_NORMAL : FMOD.MODE.LOOP_OFF,
                out o_sound
            );

            return r == FMOD.RESULT.OK;
        }

        /// <summary>
        /// Play a loaded sound on a specified channel.
        /// </summary>
        /// <param name="p_sound">A loaded sound.</param>
        /// <param name="p_channelGroup">The channel group to play on.</param>
        /// <param name="o_channel">The channel created.</param>
        /// <returns></returns>
        public bool PlaySound(FMOD.Sound p_sound, FMOD.ChannelGroup p_channelGroup, out FMOD.Channel o_channel)
        {
            FMOD.RESULT r = m_fmodSystem.playSound(
                p_sound,
                p_channelGroup,
                false,
                out o_channel
            );
            
            return r == FMOD.RESULT.OK;
        }

        public void ForceUpdate()
        {
            m_fmodSystem.update();
        }
        public uint Version
        {
            get {
                uint version = 0;
                m_fmodSystem.getVersion(out version);
                return version;
            }
        }
    }
}
