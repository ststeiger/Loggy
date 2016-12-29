using System;
using System.Collections.Generic;
using System.Web;

namespace Loggy.ajax
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für jsErrorLog
    /// </summary>
    public class jsErrorLog : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

/*
 

“ ”


https://www.redmine.org/boards/2/topics/22354

/var/lib/redmine/default/files

a (installdir)/apps/redmine/files to something else like a network path.
No configuration in redmine, but you can change the files directory 
to a symlink pointing to the other directory you want your files in.

\\corinet02\Webs$\Servicedesk\files\2016\12







            new Http.Json("SaveUpdateSibe.ashx", JSON.stringify(SaveInfo))
                    .success(function (r) {
                    console.log("saved");
                    console.log(r);
                })
                    .send();





                document.body.insertAdjacentHTML("beforeend", this.sibeTableTemplate);
                var onSuccess = function (r) {
                    r = JSON.parse(r);
                    COR.Basic.SIBE.Populate(r.data);
                    /*
                    r.data[i].SIBE_UID
                    
                    r.data[i].SIBE_Nr
                    r.data[i].SIBE_Anlage
                    r.data[i].SIBE_Brandschutzart
                    r.data[i].SIBE_AnlageKategorie
                    r.data[i].SIBE_Periode
                    r.data[i].ZO_BRAJHR_UID
                    r.data[i].ZO_BRAJHR_SIBE_UID
                    r.data[i].ZO_BRAJHR_Zustand
                    r.data[i].ZO_BRAJHR_Jahr
                    r.data[i].ZO_BRAJHR_01
                    r.data[i].ZO_BRAJHR_02
                    r.data[i].ZO_BRAJHR_03
                    r.data[i].ZO_BRAJHR_04
                    r.data[i].ZO_BRAJHR_05
                    r.data[i].ZO_BRAJHR_06
                    r.data[i].ZO_BRAJHR_v7
                    r.data[i].ZO_BRAJHR_v8
                    r.data[i].ZO_BRAJHR_09
                    r.data[i].ZO_BRAJHR_10
                    r.data[i].ZO_BRAJHR_11
                    r.data[i].ZO_BRAJHR_12
                    r.data[i].ZO_BRAJHR_Sem1
                    r.data[i].ZO_BRAJHR_Sem2
                    r.data[i].ZO_BRAJHR_Annual
                    */
                    // console.log(r.data[0].SIBE_Anlage);
                };
                // trTitleAnually.style.display = "none";
                console.log(COR);
                // trTitleAnually.style.display = "none";
                var req = new Http.Post("../ajax/Getdatatable.ashx?resource=.Quartalsreport.QREP_SIBE_Liste.sql")
                    .success(onSuccess)
                    .send();
            };





                    new Http.Post("../ajax/GetDataTable.ashx", postData)
                        .success(onSuccess)
                        .always(cbAlways)
                        .send()
                    ;








        new Http.RequestChain().add(req1)
              .whenDone(
                  function ()
                  {
                      this.fetchDataIfExists();
                  }.bind(this)
              )
              .process()
        ;


 */