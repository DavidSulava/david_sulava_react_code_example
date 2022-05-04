using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contracts.Models;
using AutoMapper;
using DesignGear.Common.Extensions;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ProductVersionController : ControllerBase
    {
        private readonly IProductVersionService _productVersionService;
        private readonly IMapper _mapper;

        public ProductVersionController(IProductVersionService productVersionService, IMapper mapper)
        {
            _productVersionService = productVersionService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<Guid> CreateProductVersion([FromForm] VmProductVersionCreate create)
        {
            return await _productVersionService.CreateProductVersionAsync(create.MapTo<ProductVersionCreateDto>(_mapper));
        }

        [HttpPut]
        public async Task UpdateProductVersion([FromForm] VmProductVersionUpdate update)
        {
            await _productVersionService.UpdateProductVersionAsync(update.MapTo<ProductVersionUpdateDto>(_mapper));
        }

        [HttpDelete]
        public async Task RemoveProductVersion(Guid id)
        {
            await _productVersionService.RemoveProductVersionAsync(id);
        }

        /*
         * todo Anton должно быть просто [HttpGet()]. В целом это метод, который возвращает фильтрованный список версий продукта. 
         * ProductId - одно из условий фильтра, могут быть еще условия (пагинация и т.д.)
         * Возможно после того как подключится фронтендер, метод изменится, если заюзаем Kendo
         */
        [HttpGet("byproduct")]
        public async Task<ICollection<VmProductVersion>> ProductVersionsByProduct(Guid productId)
        {
            return (await _productVersionService.GetProductVersionsByProductAsync(productId)).MapTo<ICollection<VmProductVersion>>(_mapper);
        }

        [HttpGet("{id}")]
        public async Task<VmProductVersion> ProductVersionById([FromRoute] Guid id)
        {
            return (await _productVersionService.GetProductVersionAsync(id)).MapTo<VmProductVersion>(_mapper);
        }

        /* 
         * todo Anton Т.к. архив модели может быть большим, то так возвращать его не стоит, потому что asp.net произведет base64 кодирование,
         * в результате чего размер ответа вырастет более чем на 30%, плюс сама операция упаковки на сервере и распаковки на клиенте будет занимать время.
         * Эффективнее возвращать непосрдественно бинарные данные. См. метод File класса ControllerBase
         * 
         * Также лучше передавать id как часть url (productversion/model/1 вместо productversion/model?id=1). 
         * Причем в идеале мы лолжны стремиться к restful. С учетом того что model - это подресурс productversion, то url должен выглядеть так:
         * productversion/{productVersionId}/model, т.к. модель у нас одна
         */
        [HttpGet("model")]
        public async Task<VmAttachment> ModelFile(Guid id)
        {
            return (await _productVersionService.GetModelFileAsync(id)).MapTo<VmAttachment>(_mapper);
        }

        /*
         *  todo Anton тоже самое что и для метода ModelFile. Возвращается только одна картинка и url должен быть такой:
         *  productversion/{productVersionId}/images/{imageId}
         */
        [HttpGet("images")]
        public async Task<ICollection<VmAttachment>> Images(Guid id)
        {
            return (await _productVersionService.GetImageFilesAsync(id)).MapTo<ICollection<VmAttachment>>(_mapper);
        }

    }
}
