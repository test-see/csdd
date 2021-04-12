using Mediator.Net.Contracts;

namespace nouns.client.profile
{
    public class DeleteClientCommand: ICommand
    {
        public int Id { get; }
        public DeleteClientCommand(int id)
        {
            Id = id;
        }
    }
}
