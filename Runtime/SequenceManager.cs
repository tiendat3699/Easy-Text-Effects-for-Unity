using System.Collections.Generic;
using MyBox;
using UnityEngine;

namespace EasyTextEffects
{
    public class SequenceManager : MonoBehaviour
    {
        public List<GameObject> slides;
    
        private int currentSlideIndex_ = -1;
    
        void Start()
        {
            foreach (var slide in slides)
            {
                slide.SetActive(false);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                NextSlide();
            }
        }

        [ButtonMethod]
        public void NextSlide()
        {
            if (currentSlideIndex_ < slides.Count - 1)
            {
                if (currentSlideIndex_ >= 0)
                    slides[currentSlideIndex_].SetActive(false);
                currentSlideIndex_++;
                slides[currentSlideIndex_].SetActive(true);
            }
            else
            {
                slides[^1].SetActive(false);
                currentSlideIndex_ = -1;
            }
        }

    }
}
