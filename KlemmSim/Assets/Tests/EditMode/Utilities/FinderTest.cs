using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;

public class FinderTest
{
    // Empty Class for testing TestMonoBehaviour related methods
    private class TestMonoBehaviour : MonoBehaviour {}

    // Testing methods that work with tags will be left out.
    // This is because Unity does not allow to access tags directly.

    [Test]
    public void FindOrCreateObjectOfType_ObjectExists_ReturnSameObject()
    {
        GameObject testObject = new GameObject();
        TestMonoBehaviour realComponent = testObject.AddComponent<TestMonoBehaviour>();

        TestMonoBehaviour foundComponent = Finder.FindOrCreateObjectOfType<TestMonoBehaviour>();

        Assert.AreEqual(realComponent, foundComponent);
    }

    [Test]
    public void FindOrCreateObjectOfType_ObjectDoesntExists_ReturnObject()
    {
        TestMonoBehaviour foundComponent = Finder.FindOrCreateObjectOfType<TestMonoBehaviour>();

        Assert.IsInstanceOf<TestMonoBehaviour>(foundComponent);
    }

    [Test]
    public void FindOrCreateComponent_ComponentExists_ReturnSameComponent()
    {
        GameObject testObject = new GameObject();
        var component = testObject.AddComponent<TestMonoBehaviour>();
     
        var foundComponent = Finder.FindOrCreateComponent<TestMonoBehaviour>(testObject);

        Assert.AreEqual(component, foundComponent);
    }

    [Test]
    public void FindOrCreateComponent_ComponentDoesntExist_ReturnComponent()
    {
        GameObject go = new GameObject();
        
        var foundComponent = Finder.FindOrCreateComponent<Component>(go);

        Assert.IsNotNull(foundComponent);
        Assert.IsInstanceOf<Component>(foundComponent);
    }

    [Test]
    public void FindOrCreateGameObjectWithName_NameExists_ReturnSameObject()
    {
        string testName = "TestName1";
        GameObject realObject = new GameObject{name = testName};

        var foundObject = Finder.FindOrCreateGameObjectWithName(testName);

        Assert.AreEqual(realObject, foundObject);
    }

    [Test]
    public void FindOrCreateGameObjectWithName_NameDoesntExist_ReturnObjectWithSameName()
    {
        string testName = "TestName2";

        var foundObject = Finder.FindOrCreateGameObjectWithName(testName);

        Assert.IsTrue(foundObject.name.Equals(testName));
    }
}
