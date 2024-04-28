using _Project.Core.Messages;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Core.LifetimeScopes
{
    [DisallowMultipleComponent]
    public sealed class RootLifetimeScope : LifetimeScope
    {
        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;
            
            ConfigureMessagePipe();
        }

        private void ConfigureMessagePipe()
        {
            var options = _builder.RegisterMessagePipe();

            _builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
            
            RegisterMessageBrokers(options);
        }
        
        private void RegisterMessageBrokers(MessagePipeOptions options)
        {
            _builder.RegisterMessageBroker<OnUiActionMessage>(options);
            
            this.LogRegisterSuccess();
        }
    }
}