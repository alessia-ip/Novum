//-----------------------------------------------------------------------
// <copyright file="AugmentedImageVisualizer.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.AugmentedImage
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using GoogleARCore;
    using GoogleARCoreInternal;
    using UnityEngine;
    using UnityEngine.Video;
    using PubNubAPI;


    public class AugmentedImageVisualizerCopy : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public AugmentedImage Image;

        /// <summary>
        /// A model for the lower left corner of the frame to place when an image is detected.
        /// </summary>
        public GameObject empty;

        public GameObject video;
        public GameObject one;
        public GameObject two;
        public GameObject three;
        public GameObject four;
        public GameObject five;
        public GameObject six;
        public GameObject seven;
        public GameObject eight;
        public GameObject nine;
        public GameObject ten;
        public GameObject eleven;
        public GameObject twelve;

        public GameObject bottomOne;
        public GameObject bottomTwo;
        public GameObject bottomThree;

        public GameObject misc1;
        public GameObject misc2;

        PubNub pubnub;
        List<string> listChannelGroups;
        List<string> listChannels;

        string cg1 = "channelGroup1";
        string cg2 = "channelGroup2";
        string ch1 = "channel1";
        string ch2 = "channel2";

        string deviceId = "aaa";
        PNPushType pnPushType = PNPushType.GCM;

        public void Start()
        {
            empty.SetActive(false);
            video.SetActive(false);
            one.SetActive(false);
            two.SetActive(false);
            three.SetActive(false);
            four.SetActive(false);
            five.SetActive(false);
            six.SetActive(false);
            seven.SetActive(false);
            eight.SetActive(false);
            nine.SetActive(false);
            ten.SetActive(false);
            eleven.SetActive(false);
            twelve.SetActive(false);
            bottomOne.SetActive(false);
            bottomTwo.SetActive(false);
            bottomThree.SetActive(false);

            Init();

        }

        void Init(){

            PNConfiguration pnConfiguration = new PNConfiguration();
            pnConfiguration.SubscribeKey = "sub-c-623edbe8-29cc-11e9-9220-327a8a7fb65a";
            pnConfiguration.PublishKey = "pub-c-8bf55402-8177-4404-afec-f9b9257b41d8";
            pnConfiguration.Secure = true;

            pubnub = new PubNub(pnConfiguration);

            pubnub.Subscribe().Channels(new List<string>() { "my_channel" }).Execute();

            pubnub.Publish()
            .Channel("my_channel")
            .Message("test message")
            .Async((result, status) => {
                if (!status.Error)
                {
                    Debug.Log(string.Format("Publish Timetoken: {0}", result.Timetoken));
                }
                else
                {
                    Debug.Log(status.Error);
                    Debug.Log(status.ErrorData.Info);
                }
           });
        }

        public void Update()
        {

            if (Image == null || Image.TrackingState != TrackingState.Tracking)
            {
                empty.SetActive(false);
                one.SetActive(false);
                two.SetActive(false);
                three.SetActive(false);
                four.SetActive(false);
                five.SetActive(false);
                six.SetActive(false);
                seven.SetActive(false);
                eight.SetActive(false);
                nine.SetActive(false);
                ten.SetActive(false);
                eleven.SetActive(false);
                twelve.SetActive(false);
                bottomOne.SetActive(false);
                bottomTwo.SetActive(false);
                bottomThree.SetActive(false);
                //video.SetActive(false);
                return;
            }
            if (Image.Name == "IntroVideo")
            {
                video.SetActive(true);
                return;

            }
            if (Image.Name == "ImageTargetOne")
            {
                one.SetActive(true);
                return;

            }
            if (Image.Name == "ImageTargetTwo")
            {
                two.SetActive(true);
                return;

            }
            if (Image.Name == "ImageTargetThree")
            {
                three.SetActive(true);
                return;

            }
            if (Image.Name == "ImageTargetFour")
            {
                four.SetActive(true);
                return;

            }
            if (Image.Name == "ImageTargetFive")
            {
                five.SetActive(true);
                return;

            }
            if (Image.Name == "ImageTargetSix")
            {
                six.SetActive(true);
                return;

            }
            if (Image.Name == "ImageTargetSeven")
            {
                seven.SetActive(true);
                return;

            }
            if (Image.Name == "ImageTargetEight")
            {
                eight.SetActive(true);
                return;

            }
            if (Image.Name == "ImageTargetNine")
            {
                nine.SetActive(true);
                return;

            }
            if (Image.Name == "ImageTargetTen")
            {
                ten.SetActive(true);
                return;

            }
            if (Image.Name == "FinalImagesEleven")
            {
                eleven.SetActive(true);
                return;

            }
            if (Image.Name == "ImageTargetTwelve")
            {
                twelve.SetActive(true);
                return;

            }
            if (Image.Name == "AmbientTargetOne")
            {
                misc1.SetActive(true);
                return;

            }
            if (Image.Name == "AmbientTargetTwo")
            {
                misc2.SetActive(true);
                return;

            }
            if (Image.Name == "ImageTargetBaseOne")
            {
                bottomOne.SetActive(true);
                return;

            }
            if (Image.Name == "ImageTargetBaseTwo")
            {
                bottomTwo.SetActive(true);
                return;

            }
            if (Image.Name == "ImageTargetBaseThree")
            {
                bottomThree.SetActive(true);
                return;
            }
        }
    }
}