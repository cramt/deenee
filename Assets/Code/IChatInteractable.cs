namespace Deenee {
    public interface IChatEntry {
        string Text { get; }
    }
    public interface IChatInteractable {
        string[] Comamnds { get; }
        uint MinArg { get; }
        uint MaxArg { get; }
        IChatEntry Call(string[] args);
    }
}