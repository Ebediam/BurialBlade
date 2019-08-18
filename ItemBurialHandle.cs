using BS;
using UnityEngine;

namespace BurialBlade
{

    public class ItemBurialHandle : MonoBehaviour
    {
       
        protected Item item;
        public Transform blade;
        public bool isExtended = false;
        public bool isChanging = false;
        public float rotation = 165f;
        public float speed = 35f;
        public float count = 0;
        public Transform bladeColliders;
        public Transform burialHandle2;
        public ObjectHolder burialHolder;


        public AudioSource trickSFX;

        protected void Awake()
        {

            item = this.GetComponent<Item>();
            item.OnHeldActionEvent += OnHeldAction;
            item.OnSnapEvent += OnHandleSnap;

            item.OnUnSnapEvent += OnHandleUnsnap;
            if (item.GetComponentInChildren<ObjectHolder>())
            {
                burialHolder = item.GetComponentInChildren<ObjectHolder>();
            }
            else
            {
                Debug.Log("Object holder not detected");
            }
            
            
            


            trickSFX = item.transform.Find("TrickSound").GetComponent<AudioSource>();

            bladeColliders = item.transform.Find("BladeColliders");

            bladeColliders.gameObject.SetActive(false);
            burialHandle2 = item.transform.Find("BurialHandle2");


        }



        public void OnHandleUnsnap(ObjectHolder holder)
        {
            if (burialHolder)
            {

               if (burialHolder.holdObjects.Count == 0)
               {
                    if (isExtended && !isChanging)
                    {
                        isChanging = true;
                    }
               }
                


            }
            
        }

        public void OnHandleSnap(ObjectHolder holder)
        {
            if (burialHolder)
            {

                if (burialHolder.holdObjects.Count == 0)
                {
                    if (isExtended && !isChanging)
                    {
                        isChanging = true;
                    }
                }



            }

        }

        public void OnHeldAction(Interactor interactor, Handle handle, Interactable.Action action)
        {

            if (action == Interactable.Action.AlternateUseStop && burialHolder.holdObjects.Count == 0)
            {
                isChanging = true;
            }
        }

        void FixedUpdate()
        {
            if (isChanging)
            {
                ChangeWeapon();
            }
            if (burialHolder)
            {
                if (item.holder && isExtended && !isChanging && burialHolder.holdObjects.Count == 0)
                {
                    isChanging = true;
                }
            }

        }

        public void ActivateColliders()
        {
            bladeColliders.gameObject.SetActive(true);
        }

        public void DeactivateColliders()
        {
            bladeColliders.gameObject.SetActive(false);
        }
        public void ChangeWeapon()
        {
            if (isExtended)
            {
                burialHandle2.Rotate(0f, 0f, rotation / speed);
                count++;
                if(count >= speed)
                {
                    trickSFX.Play();
                    count = 0f;
                    isExtended = false;
                    isChanging = false;
                                       

                }
            }
            else
            {
                burialHandle2.Rotate(0f, 0f, -rotation / speed);
                count++;
                if (count >= speed)
                {
                    
                    trickSFX.Play();
                    count = 0f;
                    isExtended = true;
                    isChanging = false;
                    
                    

                }
            }
        }

    }
}