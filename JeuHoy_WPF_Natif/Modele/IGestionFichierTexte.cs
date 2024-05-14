namespace JeuHoy_WPF_Natif.Modele
{
    /// <summary>
    /// Auteur : nicolas lajoie, Arthur
    /// description : Interface pour la gestion des fichiers texte
    /// date : 2020-05-12
    /// </summary>
    internal interface IGestionFichierTexte
    {
        /// <summary>
        /// Lecture du fichier
        /// </summary>
        /// <param name="sNomFichier"></param>
        /// <returns></returns>
        SkeletonData LireFichier(string sNomFichier);
        /// <summary>
        /// Écriture du fichier
        /// </summary>
        /// <param name="sNomFichier"></param>
        /// <param name="bodyContenu"></param>
        /// <returns></returns>
        string EcrireFichier(string sNomFichier, SkeletonData bodyContenu);
    }
}
