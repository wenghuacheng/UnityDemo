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

        //���ػص����ڳ������������
        private static Action OnLoaderCallback;

        #region ͬ����ʽ
        /// <summary>
        /// ͬ����ʽ���س���
        /// </summary>
        /// <param name="scene"></param>
        public static void Load(Scene scene)
        {
            //�ȼ��صȴ��������ڵȴ�������ʾ���ټ�����Ϸ����
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

        #region �첽��ʽ

        private static AsyncOperation asyncOperation;
        private static float progress;

        private class LoadingMonoBehavior : MonoBehaviour { }

        /// <summary>
        /// �첽��ʽ���س���
        /// </summary>
        /// <param name="scene"></param>
        public static void LoadAsync(Scene scene)
        {
            //�ȼ��صȴ��������ڵȴ�������ʾ���ټ�����Ϸ����
            OnLoaderCallback = () =>
            {
                GameObject loadingGameObject = new GameObject("");
            //��̬���в���ֱ������Э�̣�ͨ���½�������ķ�ʽ����
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

                //unity�Ľ���ֻ����ص�90%������10%�ǳ�������
                if (asyncOperation.progress >= 0.9f)
                {
                    //ֻ������Ϊtrue�Ż��л�����������ʹ�õ����������л�
                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }

        /// <summary>
        /// ��ȡ����
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