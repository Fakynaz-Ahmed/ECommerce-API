using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTO_S.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class BasketController(IServiceManager _serviceManager):ApiBaseController
    {
        //Get Basket 
        [HttpGet]  //Get BaseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> GetAsync(string Key)
        {
            var Basket =await _serviceManager.BasketService.GetBasketAsync(Key);
            return Ok(Basket);
        }

        //Create or update Basket 
        [HttpPost] //Post BaseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> CreateOrUpdateAsync(BasketDto basketDto)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basketDto);
            return Ok(Basket);
        }

        //Delete Basket
        [HttpDelete("{Key}")] //Delete BaseUrl/api/Basket/{Key as a value}
        public async Task<ActionResult<bool>> DeleteAsync(string Key)
        {
            var Result = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(Result);
        }
    }
}
