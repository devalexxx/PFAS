using PFAS.News;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PFAS.Internal
{
    [CustomEditor(typeof(NewsList))]
    public class NewsListEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            NewsList newsList = (NewsList)target;

            EditorGUILayout.LabelField("News List Editor", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            if (newsList.news == null)
                newsList.news = new List<string>();

            for (int i = 0; i < newsList.news.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                // Afficher l'index + "News"
                EditorGUILayout.LabelField($"News {i}:", GUILayout.Width(60));

                // Champ modifiable pour la string
                newsList.news[i] = EditorGUILayout.TextField(newsList.news[i]);

                // Bouton pour supprimer l'élément
                if (GUILayout.Button("X", GUILayout.Width(25)))
                {
                    newsList.news.RemoveAt(i);
                    break; // Évite les erreurs d'indexation lors de la suppression
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            // Bouton pour ajouter un nouvel élément
            if (GUILayout.Button("Ajouter une News"))
            {
                newsList.news.Add("Nouvelle news...");
            }

            // Sauvegarder les modifications
            if (GUI.changed)
            {
                EditorUtility.SetDirty(newsList);
            }
        }
    }
}
