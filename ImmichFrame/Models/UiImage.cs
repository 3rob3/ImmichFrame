﻿using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace ImmichFrame.Models
{
    public class UiImage
    {
        public Bitmap? Image { get; set; }
        public Bitmap? ThumbhashImage { get; set; }
        public Stretch ImageStretch { get; set; }
    }
}
