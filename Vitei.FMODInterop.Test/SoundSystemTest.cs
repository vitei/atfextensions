using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vitei.FMODInterop.Test
{
    [TestClass]
    public class SoundSystemTest
    {
        static SoundSystem s_system;

        [TestInitialize]
        public void StartUp()
        {
            s_system = new SoundSystem();
            s_system.Initialize();
        }
        [TestCleanup]
        public void Cleanup()
        {
            s_system.Teardown();
        }

        /// <summary>
        /// When SoundSystem.GetChannelGroup() is called with the same id,
        /// the same channel group should be returned.
        /// </summary>
        [TestMethod]
        public void GetChannelGroup_SameGroupTest()
        {
            int test = 3;
            FMOD.ChannelGroup cg1 = s_system.GetChannelGroup(test);
            Assert.IsNotNull(cg1);
            FMOD.ChannelGroup cg2 = s_system.GetChannelGroup(test);
            Assert.IsNotNull(cg2);
            Assert.AreEqual(cg1, cg2);
        }

        [TestMethod]
        public void LoadSound_LoadSuccessTest()
        {
            FMOD.Sound snd = null;
            bool bSuccess = s_system.LoadSound("TestResources/pumpuplooped.wav", true, out snd);
            Assert.IsTrue(bSuccess);
            Assert.IsNotNull(snd);
        }
    }
}
