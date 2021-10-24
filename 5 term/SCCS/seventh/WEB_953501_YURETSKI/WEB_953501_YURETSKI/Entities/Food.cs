using WEB_953501_YURETSKI.Models;

namespace WEB_953501_YURETSKI.Entities
{
    public class Food
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
        public string Description {  get; set; }
        public float Price {  get; set; }
        public int CategoryId {  get; set; }
        public Category Category {  get; set; }
        public int ImageId {  get; set; }
        public Image Image {  get; set; }
    }
}
