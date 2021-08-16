using System.Collections;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Interfaces;

namespace API.Controllers
{
    public class SecuritiesController : BaseApiController
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ISecurityService _securityService;
        private readonly IStockService _stockService;
        private readonly IUnitOfWork _unitOfWork;
        public SecuritiesController(IHttpClientFactory clientFactory,
         ISecurityService securityService,
         IStockService stockService,
         IUnitOfWork unitOfWork)
        {
            _securityService = securityService;
            _clientFactory = clientFactory;
            _stockService = stockService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetSecurities()
        {
            StockDataModel1 stockData = new StockDataModel1();

            var request = new HttpRequestMessage(HttpMethod.Get,
           "https://rest.zse.hr/web/Bvt9fe2peQ7pwpyYqODM/price-list/XZAG/2021-06-14/json");

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                stockData = await response.Content.ReadFromJsonAsync<StockDataModel1>();
            }

            return Ok(stockData);


        }
        [HttpGet("hi")]
        public async Task<ActionResult<IEnumerable>> GetSecurities1()
        {
            StockDataModel1 stockData = new StockDataModel1();

            var request = new HttpRequestMessage(HttpMethod.Get,
           "https://rest.zse.hr/web/Bvt9fe2peQ7pwpyYqODM/price-list/XZAG/2021-06-14/json");

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                stockData = await response.Content.ReadFromJsonAsync<StockDataModel1>();
            }
            foreach (var item in stockData.securities)
            {
                Convert.ToDecimal(item.close_price);
            }

            return Ok(stockData);

        }
        [HttpPut("kuki")]
        public async Task<ActionResult<List<Stock>>> ConvertStocks()
        {
            StockDataModel1 stockData = new StockDataModel1();

            var request = new HttpRequestMessage(HttpMethod.Get,
           "https://rest.zse.hr/web/Bvt9fe2peQ7pwpyYqODM/price-list/XZAG/2021-05-11/json");

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                stockData = await response.Content.ReadFromJsonAsync<StockDataModel1>();

                foreach (var item in stockData.securities)
                {
                    var list = _securityService.GetMeStocks(item.symbol, Convert.ToDecimal(item.close_price));
                }
            
            }
           

            return Ok(stockData);
        }
        //ovo šljaka, ne trebaš niš drugo!
        [HttpPut]
        public async Task<ActionResult<IEnumerable<Stock>>> ConvertStocks1()
        {
            StockDataModel1 stockData = new StockDataModel1();

            var request = new HttpRequestMessage(HttpMethod.Get,
           "https://rest.zse.hr/web/Bvt9fe2peQ7pwpyYqODM/price-list/XZAG/2021-07-27/json");

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                stockData = await response.Content.ReadFromJsonAsync<StockDataModel1>();

                var listam = await _stockService.ListAllStocksAsync();

                foreach (var item in listam)
                {
                    foreach (var subitem in stockData.securities)
                    {
                        await _securityService
                        .GetMeStocks1(subitem.symbol, Convert.ToDecimal(subitem.close_price));
                    }
                }                      
            }           
            return Ok(stockData);
        }
    }
}








