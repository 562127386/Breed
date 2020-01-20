﻿using Xamarin.Forms.Internals;

namespace Akh.Breed.Behaviors
{
    [Preserve(AllMembers = true)]
    public interface IAction
    {
        bool Execute(object sender, object parameter);
    }
}
