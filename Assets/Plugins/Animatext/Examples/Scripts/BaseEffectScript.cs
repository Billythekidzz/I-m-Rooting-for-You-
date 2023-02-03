// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    [DefaultExecutionOrder(999)]
    public class BaseEffectScript : BaseExampleScript
    {
        private static readonly string groupTextA = "<g>An</g><g>i</g><g>mat</g><g>ed</g> <color=\"#3087db\"><g>Tex</g><g>t</g></color>";
        private static readonly string groupTextB = "<g>A</g><g>nim</g><g>a</g><g>ted</g> <color=\"#3087db\"><g>Te</g><g>xt</g></color>";
        private static readonly string groupTextC = "<g>Ani</g><g>m</g><g>a</g><g>ted</g> <color=\"#3087db\"><g>Te</g><g>xt</g></color>";
        private static readonly string groupTextD = "<g>An</g><g>ima</g><g>t</g><g>ed</g> <color=\"#3087db\"><g>T</g><g>ext</g></color>";

        protected string tagName = string.Empty;
        protected float startInverval = 0;
        protected BaseEffect[] presetsA1;
        protected BaseEffect[] presetsA2;
        protected BaseEffect[] presetsA3;
        protected BaseEffect[] presetsA4;
        protected BaseEffect[] presetsB1;
        protected BaseEffect[] presetsB2;
        protected BaseEffect[] presetsB3;
        protected BaseEffect[] presetsB4;
        protected BaseEffect[] presetsC1;
        protected BaseEffect[] presetsC2;
        protected BaseEffect[] presetsC3;
        protected BaseEffect[] presetsC4;
        protected BaseEffect[] presetsD1;
        protected BaseEffect[] presetsD2;
        protected BaseEffect[] presetsD3;
        protected BaseEffect[] presetsD4;

        public GameObject exampleA1;
        public GameObject exampleA2;
        public GameObject exampleA3;
        public GameObject exampleA4;
        public GameObject exampleB1;
        public GameObject exampleB2;
        public GameObject exampleB3;
        public GameObject exampleB4;
        public GameObject exampleC1;
        public GameObject exampleC2;
        public GameObject exampleC3;
        public GameObject exampleC4;
        public GameObject exampleD1;
        public GameObject exampleD2;
        public GameObject exampleD3;
        public GameObject exampleD4;

        protected void AddAnimatexts()
        {
            AddAnimatext(exampleA1, presetsA1);
            AddAnimatext(exampleA2, presetsA2);
            AddAnimatext(exampleA3, presetsA3);
            AddAnimatext(exampleA4, presetsA4);
            AddAnimatext(exampleB1, presetsB1);
            AddAnimatext(exampleB2, presetsB2);
            AddAnimatext(exampleB3, presetsB3);
            AddAnimatext(exampleB4, presetsB4);
            AddAnimatext(exampleC1, presetsC1);
            AddAnimatext(exampleC2, presetsC2);
            AddAnimatext(exampleC3, presetsC3);
            AddAnimatext(exampleC4, presetsC4);
            AddAnimatext(exampleD1, groupTextA, presetsD1);
            AddAnimatext(exampleD2, groupTextB, presetsD2);
            AddAnimatext(exampleD3, groupTextC, presetsD3);
            AddAnimatext(exampleD4, groupTextD, presetsD4);
        }
    }
}