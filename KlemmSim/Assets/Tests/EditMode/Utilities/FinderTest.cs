using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.TestTools;

public class FinderTest
{
    // Empty Class for testing TestMonoBehaviour related methods
    private class TestMonoBehaviour : MonoBehaviour {}

    // Testing methods that work with tags will be left out.
    // This is because Unity does not allow to access tags directly.

    [Test]
    public void TestFindOrCreateObjectOfType_ObjectExists_ReturnSameObject()
    {
        GameObject testObject = new GameObject();
        TestMonoBehaviour realComponent = testObject.AddComponent<TestMonoBehaviour>();

        TestMonoBehaviour foundComponent = Finder.FindOrCreateObjectOfType<TestMonoBehaviour>();

        Assert.AreEqual(realComponent, foundComponent);
    }

    [Test]
    public void TestFindOrCreateObjectOfType_ObjectDoesntExists_ReturnObject()
    {
        TestMonoBehaviour foundComponent = Finder.FindOrCreateObjectOfType<TestMonoBehaviour>();

        Assert.IsNotInstanceOf<TestMonoBehaviour>(foundComponent);
    }

    [Test]
    public void TestFindOrCreateComponent_ComponentExists_ReturnSameComponent()
    {
        GameObject testObject = new GameObject();
        var component = testObject.AddComponent<TestMonoBehaviour>();
     
        var foundComponent = Finder.FindOrCreateComponent<TestMonoBehaviour>(testObject);

        Assert.AreEqual(component, foundComponent);
    }

    [Test]
    public void TestFindOrCreateComponent_ComponentDoesntExist_ReturnComponent()
    {
        GameObject go = new GameObject();
        
        var foundComponent = Finder.FindOrCreateComponent<Component>(go);

        Assert.IsNotNull(foundComponent);
        Assert.IsInstanceOf<Component>(foundComponent);
    }

    [Test]
    public void TestFindOrCreateGameObjectWithName_NameExists_ReturnSameObject()
    {
        string testName = "TestName1";
        GameObject realObject = new GameObject{name = testName};

        var foundObject = Finder.FindOrCreateGameObjectWithName(testName);

        Assert.AreEqual(realObject, foundObject);
    }

    [Test]
    public void TestFindOrCreateGameObjectWithName_NameDoesntExist_ReturnObjectWithSameName()
    {
        string testName = "TestName2";

        var foundObject = Finder.FindOrCreateGameObjectWithName(testName);

        Assert.IsTrue(foundObject.name.Equals(testName));
    }
}
