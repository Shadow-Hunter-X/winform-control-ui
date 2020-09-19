---
title: .NET中的事件模型
---

Raising an event from a component is no different from raising an event from a class that is not a component

### Handling and raising events

Events in .NET are based on the delegate model. The delegate model follows the observer design pattern, which enables a subscriber to register with and receive notifications from a provider. An event sender pushes a notification that an event has happened, and an event receiver receives that notification and defines a response to it. This article describes the major components of the delegate model, how to consume events in applications, and how to implement events in your code

* Events in .NET are based on the delegate mode

####  Events
An event is a message sent by an object to signal the occurrence of an action. The action can be caused by user interaction, such as a button click, or it can result from some other program logic, such as changing a property’s value. The object that raises the event is called the event sender. The event sender doesn't know which object or method will receive (handle) the events it raises. The event is typically a member of the event sender; for example, the Click event is a member of the Button class, and the PropertyChanged event is a member of the class that implements the INotifyPropertyChanged interface.

To define an event, you use the C# event or the Visual Basic Event keyword in the signature of your event class, and specify the type of delegate for the event. Delegates are described in the next section.

Typically, to raise an event, you add a method that is marked as protected and virtual (in C#) or Protected and Overridable (in Visual Basic). Name this method OnEventName; for example, OnDataReceived. The method should take one parameter that specifies an event data object, which is an object of type EventArgs or a derived type. You provide this method to enable derived classes to override the logic for raising the event. A derived class should always call the OnEventName method of the base class to ensure that registered delegates receive the event.

The following example shows how to declare an event named ThresholdReached. The event is associated with the EventHandler delegate and raised in a method named OnThresholdReached