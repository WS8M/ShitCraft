using System.Collections.Generic;

 public class ResourceRegistrar
 {
     public static ResourceRegistrar Instance;
     private List<Resource> _engagedResources;
     
     public ResourceRegistrar()
     {
         if (Instance != null)
             return;
         
         Instance = this;
         _engagedResources = new List<Resource>();
     }

     public bool IsEngaged(Resource resource) => 
         _engagedResources.Contains(resource);

     public void RegisterEngaged(Resource resource)
     {
         if (resource == null) 
             return;

         if (_engagedResources.Contains(resource)) 
             return;

         _engagedResources.Add(resource);
     }

     public void TryUnregisterEngaged(Resource resource) => 
         _engagedResources.Remove(resource);
 }