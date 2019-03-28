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



    public class AugmentedImageVisualizer : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public AugmentedImage Image;

        /// <summary>
        /// A model for the lower left corner of the frame to place when an image is detected.
        /// </summary>
        public GameObject empty;
        public GameObject artist;
        public GameObject personOne;
        public GameObject personTwo;
        public GameObject alienOne;
        public GameObject alienTwo;
        public GameObject campus;
        public GameObject spaceShip;

        GameObject uiOff;

        public void Start()
        {
            empty.SetActive(false);
            artist.SetActive(false);
            personOne.SetActive(false);
            personTwo.SetActive(false);
            alienOne.SetActive(false);
            alienTwo.SetActive(false);
            campus.SetActive(false);
            spaceShip.SetActive(false);

            uiOff = GameObject.FindWithTag("StartingVideo");

        }

        public void Update()
        {

            if (Image == null || Image.TrackingState != TrackingState.Tracking)
            {
                empty.SetActive(false);
                artist.SetActive(false);
                personOne.SetActive(false);
                personTwo.SetActive(false);
                alienOne.SetActive(false);
                alienTwo.SetActive(false);
                campus.SetActive(false);
                spaceShip.SetActive(false);
                return;
            }
            if (Image.Name == "Lanyard"){

                uiOff.SetActive(false);

            }

            if (Image.Name == "Artist")
            {
                artist.SetActive(true);
                return;

            }
            if (Image.Name == "AnswerThree")
            {
                personOne.SetActive(true);
                return;

            }
            if (Image.Name == "AnswerFour")
            {
                personTwo.SetActive(true);
                return;

            }
            if (Image.Name == "AnswerOne")
            {
                alienOne.SetActive(true);
                return;

            }
            if (Image.Name == "AnswerTwo")
            {
                alienTwo.SetActive(true);
                return;
            }
            if (Image.Name == "Campus")
            {
                campus.SetActive(true);
                return;
            }
            if (Image.Name == "Ship")
            {
                spaceShip.SetActive(true);
                return;
            }
        }
    }
}