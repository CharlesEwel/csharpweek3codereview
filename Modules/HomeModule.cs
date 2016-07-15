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

      Delete["/stylists"]=_=>{
        Stylist currentStylist = Stylist.Find(Request.Form["stylist-id"]);
        currentStylist.Delete();
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
        Dictionary<string, object> model = new Dictionary<string, object>{};
        List<Client> allClients = Client.GetAll();
        model.Add("stylist", currentStylist);
        model.Add("clients", allClients);
        return View["clients.cshtml", model];
      };

      Get["/stylist/edit/{id}"]=parameters=>{
        Stylist currentStylist = Stylist.Find(parameters.id);
        return View["stylist_edit.cshtml", currentStylist];
      };

      Patch["/stylist/edit/success"]=_=>{
        Stylist currentStylist = Stylist.Find(Request.Form["stylist-id"]);
        currentStylist.SetPrice(Request.Form["price"]);
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Delete["/clients"]=_=>{
        Client currentClient = Client.Find(Request.Form["client-id"]);
        currentClient.Delete();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        List<Client> allClients = Client.GetAll();
        string currentStylist = "All";
        model.Add("stylist", currentStylist);
        model.Add("clients", allClients);
        return View["clients.cshtml", model];
      };

      Get["/clients"]=_=>{
        Dictionary<string, object> model = new Dictionary<string, object>{};
        List<Client> allClients = Client.GetAll();
        string currentStylist = "All";
        model.Add("stylist", currentStylist);
        model.Add("clients", allClients);
        return View["clients.cshtml", model];
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
        Dictionary<string, object> model = new Dictionary<string, object>{};
        List<Client> allClients = Client.GetAll();
        string currentStylist = "All";
        model.Add("stylist", currentStylist);
        model.Add("clients", allClients);
        return View["clients.cshtml", model];
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
        Dictionary<string, object> model = new Dictionary<string, object>{};
        List<Client> allClients = Client.GetAll();
        string currentStylist = "All";
        model.Add("stylist", currentStylist);
        model.Add("clients", allClients);
        return View["clients.cshtml", model];
      };
    }
  }
}
