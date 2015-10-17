﻿namespace ArtGallery.ConsoleClient.Writers.Contracts
{
    using Common;

    public interface IWriter : IObserver
    {
        void Write(string text);

        void WriteLine(string text);
    }
}
