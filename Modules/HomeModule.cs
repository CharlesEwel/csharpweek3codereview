using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using HairSalon.Objects;
using System;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"]=_=> View["index.cshtml"];

      Delete["/"]=_=>{
        Client.DeleteAll();
        Stylist.DeleteAll();
        return View["index.cshtml"];
      };

      Get["/stylists"]=_=>{
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Get["/stylist/new"]=_=>View["stylist_new.cshtml"];

      Post["/stylist/new/success"]=_=>{
        Stylist newStylist = new Stylist(
                                          Request.Form["stylist-name"],
                                          Request.Form["price"]
                                        );
        newStylist.Save();
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Get["/stylist/{id}"]=parameters=>{
        Stylist currentStylist = Stylist.Find(parameters.id);
        List<Client> currentClients = currentStylist.GetClients();
        return View["clients.cshtml", currentClients];
      };


      Delete["/clients"]=_=>{
        Client currentClient = Client.Find(Request.Form["client-id"]);
        currentClient.Delete();
        List<Client> allClients = Client.GetAll();
        return View["clients.cshtml", allClients];
      };

      Get["/clients"]=_=>{
        List<Client> allClients = Client.GetAll();
        return View["clients.cshtml", allClients];
      };

      Get["/client/new"]=_=>{
        List<Stylist> allStylists = Stylist.GetAll();
        return View["client_new.cshtml", allStylists];
      };

      Post["/client/new/success"]=_=>{
        Client newClient = new Client(
                                          Request.Form["client-name"],
                                          Request.Form["hair-color"],
                                          Request.Form["stylist-id"]
                                        );
        newClient.Save();
        List<Client> allClients = Client.GetAll();
        return View["clients.cshtml", allClients];
      };

      Get["/stylist/{id}"]=parameters=>{
        Stylist currentStylist = Stylist.Find(parameters.id);
        List<Client> currentClients = currentStylist.GetClients();
        return View["clients.cshtml", currentClients];
      };

      Get["/client/{id}"]=parameters=>{
        Client currentClient = Client.Find(parameters.id);
        return View["client.cshtml", currentClient];
      };

      Get["/client/edit/{id}"]=parameters=>{
        Dictionary<string, object> model=new Dictionary<string, object>{};
        var currentClient = Client.Find(parameters.id);
        var allStylists = Stylist.GetAll();
        model.Add("client", currentClient);
        model.Add("stylists", allStylists);
        return View["client_edit.cshtml", model];
      };

      Patch["/client/edit/success"]=_=>{
        Client currentClient = Client.Find(Request.Form["client-id"]);
        currentClient.SetHairColor(Request.Form["hair-color"]);
        currentClient.SetStylistId(Request.Form["stylistId"]);
        List<Client> allClients = Client.GetAll();
        return View["clients.cshtml", allClients];
      };
    }
  }
}
