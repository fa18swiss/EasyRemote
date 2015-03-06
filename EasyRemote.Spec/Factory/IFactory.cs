﻿namespace EasyRemote.Spec.Factory
{
    public interface IFactory<out T> where T : class
    {
        T Create();
    }
}