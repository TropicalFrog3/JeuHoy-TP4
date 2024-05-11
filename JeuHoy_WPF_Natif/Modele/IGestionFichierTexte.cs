namespace JeuHoy_WPF_Natif.Modele
{
    internal interface IGestionFichierTexte
    {
        SkeletonData LireFichier(string sNomFichier);
        string EcrireFichier(string sNomFichier, SkeletonData bodyContenu);
    }
}
