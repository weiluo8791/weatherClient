﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.9.7.0
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

using System;
using System.Linq;
using System.Net.Http;
using Microsoft.Rest;
using WeatherServiceClientHW04;

namespace WeatherServiceClientHW04
{
    public partial class WeatherServiceHW04 : ServiceClient<WeatherServiceHW04>, IWeatherServiceHW04
    {
        private Uri _baseUri;
        
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        public Uri BaseUri
        {
            get { return this._baseUri; }
            set { this._baseUri = value; }
        }
        
        private ServiceClientCredentials _credentials;
        
        /// <summary>
        /// Credentials for authenticating with the service.
        /// </summary>
        public ServiceClientCredentials Credentials
        {
            get { return this._credentials; }
            set { this._credentials = value; }
        }
        
        private IHumidities _humidities;
        
        public virtual IHumidities Humidities
        {
            get { return this._humidities; }
        }
        
        private IPressures _pressures;
        
        public virtual IPressures Pressures
        {
            get { return this._pressures; }
        }
        
        private ITemperatures _temperatures;
        
        public virtual ITemperatures Temperatures
        {
            get { return this._temperatures; }
        }
        
        /// <summary>
        /// Initializes a new instance of the WeatherServiceHW04 class.
        /// </summary>
        public WeatherServiceHW04()
            : base()
        {
            this._humidities = new Humidities(this);
            this._pressures = new Pressures(this);
            this._temperatures = new Temperatures(this);
            this._baseUri = new Uri("https://weatherservicehw04.azurewebsites.net:443");
        }
        
        /// <summary>
        /// Initializes a new instance of the WeatherServiceHW04 class.
        /// </summary>
        /// <param name='handlers'>
        /// Optional. The set of delegating handlers to insert in the http
        /// client pipeline.
        /// </param>
        public WeatherServiceHW04(params DelegatingHandler[] handlers)
            : base(handlers)
        {
            this._humidities = new Humidities(this);
            this._pressures = new Pressures(this);
            this._temperatures = new Temperatures(this);
            this._baseUri = new Uri("https://weatherservicehw04.azurewebsites.net:443");
        }
        
        /// <summary>
        /// Initializes a new instance of the WeatherServiceHW04 class.
        /// </summary>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The set of delegating handlers to insert in the http
        /// client pipeline.
        /// </param>
        public WeatherServiceHW04(HttpClientHandler rootHandler, params DelegatingHandler[] handlers)
            : base(rootHandler, handlers)
        {
            this._humidities = new Humidities(this);
            this._pressures = new Pressures(this);
            this._temperatures = new Temperatures(this);
            this._baseUri = new Uri("https://weatherservicehw04.azurewebsites.net:443");
        }
        
        /// <summary>
        /// Initializes a new instance of the WeatherServiceHW04 class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The set of delegating handlers to insert in the http
        /// client pipeline.
        /// </param>
        public WeatherServiceHW04(Uri baseUri, params DelegatingHandler[] handlers)
            : this(handlers)
        {
            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }
            this._baseUri = baseUri;
        }
        
        /// <summary>
        /// Initializes a new instance of the WeatherServiceHW04 class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Credentials for authenticating with the service.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The set of delegating handlers to insert in the http
        /// client pipeline.
        /// </param>
        public WeatherServiceHW04(ServiceClientCredentials credentials, params DelegatingHandler[] handlers)
            : this(handlers)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            this._credentials = credentials;
            
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the WeatherServiceHW04 class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='credentials'>
        /// Required. Credentials for authenticating with the service.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The set of delegating handlers to insert in the http
        /// client pipeline.
        /// </param>
        public WeatherServiceHW04(Uri baseUri, ServiceClientCredentials credentials, params DelegatingHandler[] handlers)
            : this(handlers)
        {
            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            this._baseUri = baseUri;
            this._credentials = credentials;
            
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
        }
    }
}
