using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Net_Neuralab_Utilities
{
    /// <summary>
    /// kalsa za preuzimenje i pohranu podataka na FTP server
    /// </summary>
    class ftp
    {
        private string host;
        /// <summary>
        /// host je potpuna adresa hosta (user@host), ako želimo pohraniti u neki određeni folder na serveru dodajemo putanju u naziv hosta (user@host/folder)
        /// </summary>
        private string user;
        private string pass;
        /// <summary>
        /// user je korisničko ime, a pass šifra korisnika na poslužitelju
        /// </summary>
        
        public ftp(string host,string uName,string pass) {
            this.host = host;
            this.user = uName;
            this.pass = pass;
        }

        /// <summary>
        /// metoda za dohvačanje podataka s ftp servera, potrebno je zadati naziv podatka na serveru, drugi argument je putanja za pohranu na lokalnom računalu 
        /// </summary>
        /// <param name="src"></param>
        public void getFrom(string src,string loc)
        {
            //postavljanje veze
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://"+host+"/"+ src);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(user, pass);
            //preuzimanje podataka
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            //obrada i pohrana
            Stream responseStream = response.GetResponseStream();
            System.IO.FileInfo file = new System.IO.FileInfo(loc);
            file.Directory.Create();
            using (Stream s = File.Create(loc+"/"+src))
            {
                responseStream.CopyTo(s);
            }
            response.Close();
        }
        /// <summary>
        /// preuzima podatak definiran prvim arhumentom s lokalnog računala zadanu drugim argumentom, i pohranjuje na server
        /// </summary>
        /// <param name="name"></param>
        public void setTo(string name,string path)
        {

            // postavljanje veze
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + host + "/" + name);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(user, pass);

            // priprema podatka
            StreamReader sourceStream = new StreamReader(path+"/"+name);
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            request.ContentLength = fileContents.Length;
            //slanje
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();
        }
    }
}
