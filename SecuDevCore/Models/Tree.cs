namespace SecuDevCore.Models
{
    public class Tree
    {
        public string text { get; set; }
        public List<Tree> nodes { get; set; }
        public Tree() { 
        
            nodes = new List<Tree>();
        
        }
    }

}
