using BS;
using UnityEngine;

namespace BurialBlade
{

    public class ItemBurialBlade : MonoBehaviour
    {
       
        protected Item item;

        public Item burialHandle = null;
        public ItemBurialHandle handleController = null;
        public ParticleSystem sparks;
        public AudioSource burialBladeSnap;
        public bool snaped;

        protected void Awake()
        {

            item = this.GetComponent<Item>();
            item.OnSnapEvent += OnBurialBladeSnap;
            item.OnUnSnapEvent += OnBurialBladeUnsnap;
            sparks = item.transform.Find("Sparks").GetComponent<ParticleSystem>();
            burialBladeSnap = item.transform.Find("Audio Source").GetComponent<AudioSource>();



            
        }

        void OnBurialBladeUnsnap(ObjectHolder holder)
        {

            if (!handleController)
            {
                InitializeHolder(holder);
            }
            
            if (handleController)
            {
                if(holder == handleController.burialHolder)
                {
                    handleController.DeactivateColliders();
                    sparks.Play();
                    burialBladeSnap.Play();
                }


            }
        }
        void OnBurialBladeSnap(ObjectHolder holder)
        {

            if (!handleController)
            {
                InitializeHolder(holder);
            }

            if (handleController)
            {
                if (holder == handleController.burialHolder)
                {
                    handleController.ActivateColliders();
                    sparks.Play();
                    burialBladeSnap.Play();
                    if (!handleController.isExtended)
                    {
                        handleController.isChanging = true;
                    }
                }

            }


        }

        public void OnHeldAction(Interactor interactor, Handle handle, Interactable.Action action)
        {
           
        }

        public void InitializeHolder(ObjectHolder holder)
        {
            if (holder.name == "BurialBladeHolder")
            {

                if (holder.transform)
                {
                    if (holder.transform.parent)
                    {
                        if (holder.transform.parent.gameObject)
                        {
                            if (holder.transform.parent.gameObject.GetComponent<Item>())
                            {
                                burialHandle = holder.transform.parent.gameObject.GetComponent<Item>();
                                if (burialHandle.GetComponent<ItemBurialHandle>())
                                {

                                    handleController = burialHandle.GetComponent<ItemBurialHandle>();
                                    handleController.ActivateColliders();
                                    sparks.Play();
                                    burialBladeSnap.Play();


                                }
                                else
                                {
                                    Debug.Log("ItemBurialHandle script not found");
                                }
                            }
                            else
                            {
                                Debug.Log("Item component of the parent gameObject not found");
                            }
                        }
                        else
                        {
                            Debug.Log("Holder parent gameobject not recognized");
                        }
                    }
                    else
                    {
                        Debug.Log("Holder parent transform not recognized");
                    }
                }
                else
                {
                    Debug.Log("Holder transform not recognized");
                }




            }
            else
            {
                Debug.Log("Holder not recognized");
            }
        }

        void FixedUpdate()
        {
            

        }

    

    }
}