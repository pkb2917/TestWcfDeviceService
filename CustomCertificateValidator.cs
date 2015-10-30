using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Reflection;
using System.Configuration;
using System.IO;

namespace TestWcfDeviceService
{
    public class CustomCertificateValidator : X509CertificateValidator
    {
        X509Certificate2 validCertificate;

        public CustomCertificateValidator()
        {
            validCertificate = new X509Certificate2();
        }

        public CustomCertificateValidator(X509Certificate2 cert)
        {
            validCertificate = cert;
        }

        private bool ValidateCertificate(X509Certificate2 certificate, out string validationFailedMsg)
        {            

            validationFailedMsg = null;

            if (certificate == null || string.IsNullOrEmpty(certificate.Subject) || string.IsNullOrEmpty(certificate.SerialNumber))
            {
                validationFailedMsg = "Invalid Certificate.";
                return false;
            }

            // Check the issuer
            if (string.Compare(validCertificate.Issuer, certificate.Issuer) != 0)
            {
                validationFailedMsg = "Invalid Certificate.";
                return false;
            }

            // Check the expiry date of the certificate
            string certExpiryDate = certificate.GetExpirationDateString();
            int? certExpiredDays = null;

            if (!string.IsNullOrEmpty(certExpiryDate))
                certExpiredDays = DateTime.Compare(DateTime.Now, DateTime.Parse(certExpiryDate));

            if (!certExpiredDays.HasValue)
            {
                validationFailedMsg = "Invalid Certificate.";
                return false;
            }
            else if (certExpiredDays.Value > 0)
            {
                validationFailedMsg = "Certificate has expired.";
                return false;
            }
           
            return true;
        }

        public override void Validate(X509Certificate2 certificate)
        {
            
        }
    }
}
