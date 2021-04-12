using Mediator.Net.Contracts;

namespace nouns.client.profile
{
    public class DeleteClientEntity : ICommand
    {
        public int Id { get; }
        public DeleteClientEntity(int id)
        {
            Id = id;
        }
    }
}
