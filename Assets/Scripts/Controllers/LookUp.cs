using System;
using System.Collections;
using Models;
using UnityEngine;


namespace Models
{
    public class Lookup
    {
        private static Lookup instance;

        public static Lookup Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Lookup();
                }

                return instance;
            }
            set => instance = value;

        }

        private Lookup()
        {
        }

        //Models

        #region
        
        public IGameInitiator GameInitiator;
        
        #endregion

        //Controllers

        #region


        #endregion

        //Views

        #region

        #endregion


    }
}

