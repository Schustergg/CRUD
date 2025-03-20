namespace Crud.Business.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
    }
}
