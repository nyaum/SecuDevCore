namespace SecuDevCore.Models
{
    public class Tree
    {
        public int LocationID { get; set; }
        public int? ParentLocationID {  get; set; }
        public string LocationName { get; set; }
        public string text { get; set; }
        public List<Tree> nodes { get; set; }
        public Tree() {

            nodes = null;
        
        }
    }

}
