﻿using Game399.Shared;

namespace Game399.Tests;

public class ObservableValueTests
{
    [Test]
    public void Add_Handler_Invokes_Immediately()
    {
        var ov = new ObservableValue<int>(10);
        int calls = 0;
        ov.ChangeEvent += _ => calls++;
        Assert.That(calls, Is.EqualTo(1));
    }

    [Test]
    public void ChangeEvent_Raised_On_Set_When_Value_Differs()
    {
        var ov = new ObservableValue<int>(10);
        int calls = 0;
        ov.ChangeEvent += _ => calls++; // immediate call -> 1

        ov.Value = 20; // change -> +1

        Assert.That(calls, Is.EqualTo(2));
        Assert.That(ov.Value, Is.EqualTo(20));
    }

    [Test]
    public void Does_Not_Notify_When_Same_Value()
    {
        var ov = new ObservableValue<string>("a");
        int calls = 0;
        ov.ChangeEvent += _ => calls++; // immediate call -> 1

        ov.Value = "a"; // same value, should not call again

        Assert.That(calls, Is.EqualTo(1));
    }

    [Test]
    public void Remove_Handler_Stops_Further_Notifications()
    {
        var ov = new ObservableValue<int>(5);
        int calls = 0;
        void Handler(int _) { calls++; }

        ov.ChangeEvent += Handler; // immediate call -> 1
        ov.ChangeEvent -= Handler; // unsubscribe

        ov.Value = 7; // should not invoke handler

        Assert.That(calls, Is.EqualTo(1));
    }

    [Test]
    public void Equals_Compares_To_Inner_Value()
    {
        var ov = new ObservableValue<int>(42);
        Assert.That(ov.Equals(42), Is.True);
        Assert.That(ov.Equals(7), Is.False);
        Assert.That(ov.GetHashCode(), Is.EqualTo(EqualityComparer<int>.Default.GetHashCode(42)));
    }
}
