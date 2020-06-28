﻿using System;
using System.Data;
using NUnit.Framework;

public class stats_tests 
{
    [Test]
    public void can_add()
    {
        Stats stats = new Stats();
        stats.Add(StatType.MoveSpeed, 3f);
        
        Assert.AreEqual( 3f, stats.Get(StatType.MoveSpeed));
        
        stats.Add(StatType.MoveSpeed, 5f);
        Assert.AreEqual( 8f, stats.Get(StatType.MoveSpeed));
    }

    [Test]
    public void can_remove()
    {
        Stats stats = new Stats();
        stats.Add(StatType.MoveSpeed, 3f);

        Assert.AreEqual(3f, stats.Get(StatType.MoveSpeed));
        
        stats.Remove(StatType.MoveSpeed, 3f);
        Assert.AreEqual(0f, stats.Get(StatType.MoveSpeed));

    }
}