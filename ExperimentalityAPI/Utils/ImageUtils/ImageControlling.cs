using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExperimentalityAPI.Utils.ImageUtils
{
    public class ImageControlling
    {
        public IFormFile resizeImage(IFormFile originFile)
        {
            IFormFile resultFile = originFile;
            var image = Image.Load(originFile.OpenReadStream());
            var size = this.calculateMaxSize(image.Width, image.Height, 100);
            image.Mutate(x => x.Resize((image.Width *(size-6)/ 100), (image.Height *(size-6)/ 100)));
            var memoryStream = new MemoryStream();
            if (originFile.FileName.Split('.').ElementAt(1) == "png") 
            {
                image.Save(memoryStream, new PngEncoder());
            }
            else
            {
                image.Save(memoryStream, new JpegEncoder());
            }
            
            resultFile = new FormFile(memoryStream, 0, memoryStream.Length, originFile.Name, originFile.FileName);
            return resultFile;
        }

        public int calculateMaxSize(int width, int height, int size)
        {
            if ((width * (size - 2) /100) * (height * (size - 2)/ 100 ) < 1048576)
                return size - 2;
            else
                return this.calculateMaxSize(width, height, size - 2);
        }
    }
}
