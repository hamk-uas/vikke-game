using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using Object = UnityEngine.Object;

namespace Tests
{
    public class PlayTestSuite
    {
        #region StorylistItem

        [UnityTest]
        [Category("UnitTest")]
        public IEnumerator StorylistItem_Select_Story_Test()
        {
            //Create a blank canvas
            Canvas canvas = CreateTestGameObject<Canvas>();

            //Fetch a dummy story
            Story story = GetStories()[0];//GetResources<Story>("Content/Stories/DummyStory");

            //Fetch prefab from resources
            StorylistItem storylistItem = GetResources<StorylistItem>("Prefabs/UI elements/StorylistItem");
            //Instantiate the prefab to the canvas
            Object.Instantiate(storylistItem, canvas.transform);

            //Get references to the storylistitem content components
            Text title = storylistItem.transform.Find("txtStorylistItemTitle").GetComponent<Text>();
            Text description = storylistItem.transform.Find("txtStorylistItemDesc").GetComponent<Text>();
            Button button = storylistItem.GetComponent<Button>();

            //Create a substitute for the item select callback
            IOnStoryItemSelected callback = Substitute.For<IOnStoryItemSelected>();

            //Construct the list item
            storylistItem.Constructor(title, description);
            storylistItem.SetItemContent(story, callback);

            //Simulate a click on the item
            button.onClick.Invoke();

            //Move to the next frame
            yield return null;

            //Check if the selection was registered
            callback.Received().OnStoryItemSelected(story);
        }

        #endregion

        #region UIManager
        /*
         * UIManager tests don't work :(
         * Idea is to make sure the source prefab and the prefab that was spawned from navigation are the same
         * However, GetCorrespondingObjectFromSource always returns null
         */

        [UnityTest]
        //[Category("UnitTest")]
        public IEnumerator UIManager_Navigation_Test_Find_View_In_Canvas()
        {
            Canvas canvas = CreateTestGameObject<Canvas>();
            NavigationManager uiManager = CreateTestGameObject<NavigationManager>();

            GameObject startView = Resources.Load<GameObject>("Prefabs/Views/StartView");
            GameObject storylistView = Resources.Load<GameObject>("Prefabs/Views/StorylistView");
            Object.Instantiate(startView, canvas.transform);

            List<NavigationPath> pathContainers = GetNavigationPaths();

            uiManager.Constructor(startView, canvas, pathContainers);

            yield return null;

            uiManager.Navigate(startView, NavigationDirection.FORWARD);

            yield return null;

            //StorylistView storylistView = Resources.Load<StorylistView>("Prefabs/Views/StorylistView");
            //storylistView.gameObject.name =+ "(Clone)";

            //foreach (NavigationPathContainer item in pathContainers)
            //{
            //    if (item. == "StorylistView")
            //        gameObject = item.gameObject;
            //}

            GameObject tempGameObject = CreateTestGameObject();

            foreach (Transform childTransform in canvas.transform)
                if (childTransform.name == "StorylistView(Clone)")
                    tempGameObject = childTransform.gameObject;

            GameObject prefabParent = PrefabUtility.GetCorrespondingObjectFromSource(tempGameObject);

            Assert.AreEqual(storylistView, prefabParent);
        }

        [UnityTest]
        //[Category("UnitTest")]
        public IEnumerator UIManager_Navigation_Test_Active_View()
        {
            Canvas canvas = CreateTestGameObject<Canvas>();
            NavigationManager uiManager = CreateTestGameObject<NavigationManager>();

            GameObject startView = Resources.Load<GameObject>("Prefabs/Views/StartView");
            GameObject storylistView = Resources.Load<GameObject>("Prefabs/Views/StorylistView");
            Object.Instantiate(startView, canvas.transform);

            List<NavigationPath> pathContainers = GetNavigationPaths();

            uiManager.Constructor(startView, canvas, pathContainers);

            yield return null;

            uiManager.Navigate(startView, NavigationDirection.FORWARD);

            yield return null;

            //StorylistView storylistView = Resources.Load<StorylistView>("Prefabs/Views/StorylistView");
            //storylistView.gameObject.name =+ "(Clone)";

            //foreach (NavigationPathContainer item in pathContainers)
            //{
            //    if (item. == "StorylistView")
            //        gameObject = item.gameObject;
            //}

            GameObject activeView = uiManager.GetActiveView();
            GameObject parentPrefab = PrefabUtility.GetCorrespondingObjectFromSource(activeView);

            //foreach (Transform transform in canvas.transform)
            //    if (transform.name == "StorylistView(Clone)")
            //        gameObject = PrefabUtility.GetCorrespondingObjectFromSource(transform.gameObject);

            Assert.AreEqual(storylistView, parentPrefab);
        }
        #endregion

        #region Storylist

        [UnityTest]
        [Category("UnitTest")]
        public IEnumerator StorylistView_Storyitem_Spawn_Test()
        {
            //Create a blank canvas
            Canvas canvas = CreateTestGameObject<Canvas>();

            //Create a gameobject to hold story items
            GameObject itemContainer = CreateTestGameObject();
            Object.Instantiate(itemContainer, canvas.transform);

            //Fetch the StorylistItem prefab asset
            StorylistItem storylistItem = GetResources<StorylistItem>("Prefabs/UI elements/StorylistItem");

            //Fetch a list of stories
            List<Story> stories = GetStories();

            //Fetch the StorylistView prefab asset
            StorylistView storylistView = GetResources<StorylistView>("Prefabs/Views/StorylistView");
            //Construct the storylistview
            storylistView.Construct(itemContainer, storylistItem, stories);
            //Instantiate the storylistview prefab to the scene
            Object.Instantiate(storylistView, canvas.transform);

            //Move to next frame
            yield return null;

            //Check that the amount of stories given to the view match the amount of stories that have been spawned
            Assert.AreEqual(stories.Count, itemContainer.transform.childCount);
        }

        //[UnityTest]
        //[Category("UnitTest")]
        //public IEnumerator StorylistView_Storyitem_Spawn_Test()
        //{
        //    //Create a blank canvas
        //    Canvas canvas = CreateTestGameObject<Canvas>();

        //    //Create a gameobject to hold story items
        //    GameObject itemContainer = CreateTestGameObject();
        //    Object.Instantiate(itemContainer, canvas.transform);

        //    //Fetch the StorylistItem prefab asset
        //    StorylistItem storylistItem = GetResources<StorylistItem>("Prefabs/UI elements/StorylistItem");

        //    //Fetch a list of stories
        //    List<Story> stories = GetStories();

        //    //Fetch the StorylistView prefab asset
        //    StorylistView storylistView = GetResources<StorylistView>("Prefabs/Views/StorylistView");
        //    //Construct the storylistview
        //    storylistView.Construct(itemContainer, storylistItem, stories);
        //    //Instantiate the storylistview prefab to the scene
        //    Object.Instantiate(storylistView, canvas.transform);

        //    //Move to next frame
        //    yield return null;

        //    //Create a substitute for the story select callback
        //    IOnStorylistStorySelect callback = Substitute.For<IOnStorylistStorySelect>();
        //    //Set callback
        //    storylistView.SetStorySelectCallback(callback);
        //}
        #endregion

        #region StoryModel
        
        [Test]
        [Category("UnitTest")]
        public void StoryModel_Initialization_Test()
        {
            //Fetch stories and get the first one from the list
            Story story = GetStories()[0];
            //Create a new StoryModel
            new StoryModel(story);
            //Test that the Instance gets initialized properly
            StoryModel storyModel = StoryModel.GetInstance();

            //Fetch a list of locations from the model
            List<Location> modelLocations = storyModel.GetStoryLocations();

            //Iterate through the locations fetched from Model
            int i = 0;
            foreach (var location in modelLocations)
            {
                //Check that all locations match
                Assert.AreSame(story.locations[i], modelLocations[i]);
                i++;
            }
        }

        [Test]
        [Category("UnitTest")]
        public void StoryModel_Set_Active_Story_Test()
        {
            //Fetch stories and get the first one from the list
            Story initialStory = GetStories()[0];
            Story newStory = GetStories()[1];

            //Create a new StoryModel
            new StoryModel(initialStory);
            //Test that the Instance gets initialized properly
            StoryModel storyModel = StoryModel.GetInstance();

            //Change active story in Model
            storyModel.SetActiveStory(newStory);

            //Fetch a list of locations from the model
            List<Location> modelLocations = storyModel.GetStoryLocations();

            //Iterate through the locations fetched from Model
            int i = 0;
            foreach (var location in modelLocations)
            {
                //Check that all locations match
                Assert.AreSame(newStory.locations[i], modelLocations[i]);
                i++;
            }
        }

        #endregion

        [TearDown]
        public void ClearAfterTest()
        {
            Scene scene = SceneManager.GetActiveScene();
            GameObject[] gameObjects = scene.GetRootGameObjects();
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.tag == "TestObject" || gameObject.tag == "ViewPrefab")
                    Object.Destroy(gameObject);
            }
        }

        #region Private functions

        /// <summary>
        /// Get a list of navigation paths
        /// </summary>
        /// <returns>List of navigation path containers</returns>
        private List<NavigationPath> GetNavigationPaths()
        {
            GameObject startView = Resources.Load<GameObject>("Prefabs/Views/StartView");
            GameObject storylistView = Resources.Load<GameObject>("Prefabs/Views/StorylistView");

            List<NavigationPath> uiPaths = new List<NavigationPath>
            {
                new NavigationPath { CallerView = startView, Direction = NavigationDirection.FORWARD, DestinationView = storylistView },
                new NavigationPath { CallerView = storylistView, Direction = NavigationDirection.BACK, DestinationView = startView }
            };

            return uiPaths;
        }

        /// <summary>
        /// Get a list of stories
        /// </summary>
        /// <returns></returns>
        private List<Story> GetStories()
        {
            List<Story> stories = new List<Story>
            {
                Resources.Load<Story>("Content/Hattula/Hattula"),
                Resources.Load<Story>("Content/Prehistory/Prehistory")
            };
            return stories;
        }

        /// <summary>
        /// Create a <see cref="GameObject"/> with a TestObject tag
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T CreateTestGameObject<T>() where T : Component
        {
            T testObject = new GameObject().AddComponent<T>();
            testObject.tag = "TestObject";
            return testObject;
        }

        /// <summary>
        /// Create an empty <see cref="GameObject"/> with a TestObject tag
        /// </summary>
        /// <returns><see cref="GameObject"/></returns>
        private GameObject CreateTestGameObject()
        {
            GameObject testObject = new GameObject { tag = "TestObject" };
            return testObject;
        }

        /// <summary>
        /// Attemt to load an Asset from Resources
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">Path of desired Asset</param>
        /// <returns><typeparamref name="T"/></returns>
        private T GetResources<T>(string path) where T : Object
        {
            T asset = Resources.Load<T>(path);
            if (asset != null)
                return asset;
            else
                throw new ArgumentException($"No resource found at path \"{path}\"");
        }

        #endregion
    }
}
