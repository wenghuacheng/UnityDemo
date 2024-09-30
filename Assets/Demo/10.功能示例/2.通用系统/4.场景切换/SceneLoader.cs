using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Demo.Other.SceneDemo
{
    public static class SceneLoader
    {
        public enum Scene
        {
            GameScene,
            Loading,
            MainScene
        }

        //加载回调，在场景结束后调用
        private static Action OnLoaderCallback;

        #region 同步方式
        /// <summary>
        /// 同步方式加载场景
        /// </summary>
        /// <param name="scene"></param>
        public static void Load(Scene scene)
        {
            //先加载等待场景，在等待场景显示后再加载游戏场景
            OnLoaderCallback = () =>
            {
                SceneManager.LoadScene(scene.ToString());
            };

            SceneManager.LoadScene(Scene.Loading.ToString());
        }

        public static void LoaderCallback()
        {
            OnLoaderCallback?.Invoke();
            OnLoaderCallback = null;
        }
        #endregion

        #region 异步方式

        private static AsyncOperation asyncOperation;
        private static float progress;

        private class LoadingMonoBehavior : MonoBehaviour { }

        /// <summary>
        /// 异步方式加载场景
        /// </summary>
        /// <param name="scene"></param>
        public static void LoadAsync(Scene scene)
        {
            //先加载等待场景，在等待场景显示后再加载游戏场景
            OnLoaderCallback = () =>
            {
                GameObject loadingGameObject = new GameObject("");
            //静态类中不能直接启动协程，通过新建对象类的方式启动
            loadingGameObject.AddComponent<LoadingMonoBehavior>().StartCoroutine(LoadSceneAsync(scene));
            };
            SceneManager.LoadScene(Scene.Loading.ToString());
        }

        private static IEnumerator LoadSceneAsync(Scene scene)
        {
            asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                yield return null;

                //unity的进度只会加载到90%，后续10%是场景呈现
                if (asyncOperation.progress >= 0.9f)
                {
                    //只有设置为true才会切换场景，可以使用点击任意键后切换
                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }

        /// <summary>
        /// 获取进度
        /// </summary>
        /// <returns></returns>
        public static float GetLoadingProgress()
        {
            if (asyncOperation != null)
                return asyncOperation.progress / 0.9f;
            else
                return 0f;
        }

        #endregion
    }
}