namespace Core.Areas.Dashboard.Entities
{
    public class StateTranslation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Locale { get; set; }
        public int StateId { get; set; }
        
        public virtual State State { get; set; }
    }
}