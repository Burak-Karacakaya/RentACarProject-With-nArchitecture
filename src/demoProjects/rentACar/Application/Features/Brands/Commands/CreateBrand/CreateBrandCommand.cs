using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand : IRequest<CreatedBrandDto>
    {
        public string Name { get; set; }

        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly BrandBusinessRules _brandBusinessRules;
            private readonly IMapper _mapper;

            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                //Business Rule kodu buraya.
                await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);


                Brand mappedBrand = _mapper.Map<Brand>(request); // Gelen request'i Brand nesnesine çevir
                Brand createdBrand = await _brandRepository.AddAsync(mappedBrand); // Hangi nesne ise onun Repository'si(BrandRepository) kullanılarak ekleme işlemini yapıyoruz.
                CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrand); // veritabanından geleni nesneyi Dto'ya çeviriyoruz.Defensive programmin için avantajlı.

                return createdBrandDto; // Dto'yu söz verdiğimiz gibi döndürüyoruz.
            }
        }
    }
}
