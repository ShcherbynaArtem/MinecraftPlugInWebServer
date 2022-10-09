﻿using AutoMapper;
using DataAccess;
using DataTransferObjects;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class WebServerService : IWebServerService
    {
        private readonly IWebServerRepository _appServerRepo;
        private readonly IMapper _mapper;

        public WebServerService(IWebServerRepository appServerRepo, IMapper mapper)
        {
            _appServerRepo = appServerRepo ?? throw new ArgumentNullException(nameof(appServerRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region user actions

        public async Task<bool> CreateUser(UserDTO userDTO)
        {
            UserEntity userEntity = _mapper.Map<UserEntity>(userDTO);
            int rowsAffected = await _appServerRepo.CreateUser(userEntity);
            if(rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<UserDTO> GetUser(Guid id)
        {
            UserEntity userEntity = await _appServerRepo.GetUserById(id);
            if(!string.IsNullOrEmpty(userEntity.username))
            {
                UserDTO userDTO = _mapper.Map<UserDTO>(userEntity);
                return userDTO;
            }
            return new UserDTO();
        }

        public async Task<bool> UpdateUser(UserDTO userDTO)
        {
            UserEntity userEntity = _mapper.Map<UserEntity>(userDTO);
            int rowsAffected = await _appServerRepo.UpdateUser(userEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            int rowsAffected = await _appServerRepo.DeleteUser(id);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        #endregion user actions

        #region product actions

        public async Task<bool> CreateProduct(ProductDTO productDTO)
        {
            ProductEntity productEntity = _mapper.Map<ProductEntity>(productDTO);
            int rowsAffected = await _appServerRepo.CreateProduct(productEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> UpdateProduct(ProductDTO productDTO)
        {
            ProductEntity productEntity = _mapper.Map<ProductEntity>(productDTO);
            int rowsAffected = await _appServerRepo.UpdateProduct(productEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            int rowsAffected = await _appServerRepo.DeleteProduct(id);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<ProductDTO> GetProduct(Guid id)
        {
            ProductEntity productEntity = await _appServerRepo.GetProductById(id);
            if (!string.IsNullOrEmpty(productEntity.name))
            {
                ProductDTO productDTO = _mapper.Map<ProductDTO>(productEntity);
                return productDTO;
            }
            return new ProductDTO();
        }

        public async Task<List<ProductDTO>> GetProducts()
        {
            List<ProductEntity> productEntities = await _appServerRepo.GetProducts();
            List<ProductDTO> productDTOs = new List<ProductDTO>();
            foreach (ProductEntity productEntity in productEntities)
            {
                productDTOs.Add(_mapper.Map<ProductDTO>(productEntity));
            }
            return productDTOs;
        }

        #endregion product actions

        #region bundle actions

        public async Task<bool> CreateBundle(BundleDTO bundleDTO)
        {
            BundleEntity bundleEntity = _mapper.Map<BundleEntity>(bundleDTO);
            Guid createdBundleId = await _appServerRepo.CreateBundle(bundleEntity);
            if (createdBundleId == Guid.Empty)
                return false;

            if(!await AddProductsToBundle(bundleEntity.id, bundleEntity.product_ids))
            {
                Console.WriteLine("Products addition failed!");
            }

            return true;
        }

        public async Task<bool> UpdateBundle(BundleDTO bundleDTO)
        {
            BundleEntity bundleEntity = _mapper.Map<BundleEntity>(bundleDTO);
            int rowsAffected = await _appServerRepo.UpdateBundle(bundleEntity);
            if (rowsAffected != 1)
                return false;
            return true;
        }

        public async Task<bool> DeleteBundle(Guid id)
        {
            //ADD CASCADE DELETION

            int rowsAffected = await _appServerRepo.DeleteBundle(id);
            if (rowsAffected == 0)
                return false;
            return true;
        }

        public async Task<BundleDTO> GetBundle(Guid id)
        {
            BundleEntity bundleEntity = await _appServerRepo.GetBundleById(id);
            if (!string.IsNullOrEmpty(bundleEntity.name))
            {
                BundleDTO bundleDTO = _mapper.Map<BundleDTO>(bundleEntity);
                return bundleDTO;
            }
            return new BundleDTO();
        }

        public async Task<List<BundleDTO>> GetBundles()
        {
            List<BundleEntity> bundleEntities = await _appServerRepo.GetBundles();
            List<BundleDTO> bundleDTOs = new List<BundleDTO>();
            foreach (BundleEntity bundleEntity in bundleEntities)
            {
                bundleDTOs.Add(_mapper.Map<BundleDTO>(bundleEntity));
            }

            return bundleDTOs;
        }

        #endregion bundle actions

        #region bundle product actions

        public async Task<bool> AddProductToBundle(BundleProductDTO bundleProductDTO)
        {
            BundleProductEntity bundleProductEntity = _mapper.Map<BundleProductEntity>(bundleProductDTO);
            int rowsAffected = await _appServerRepo.AddProductToBundle(bundleProductEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }
        public async Task<bool> DeleteProductFromBundle(BundleProductDTO bundleProductDTO)
        {
            BundleProductEntity bundleProductEntity = _mapper.Map<BundleProductEntity>(bundleProductDTO);
            int rowsAffected = await _appServerRepo.DeleteProductFromBundle(bundleProductEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        private async Task<bool> AddProductsToBundle(Guid bundle_id, IEnumerable<Guid> productIds)
        {
            List<BundleProductEntity> bundleProductEntities = new List<BundleProductEntity>();
            foreach(Guid productId in productIds)
            {
                bundleProductEntities.Add(new BundleProductEntity(bundle_id, productId));
            }

            var rowsAffected = await _appServerRepo.AddProductsToBundle(bundleProductEntities);
            if (rowsAffected != 0)
                return true;
            return false;
        }

        #endregion bundle product actions

        #region department actions

        public async Task<bool> CreateDepartment(CreateDepartmentDTO departmentDTO)
        {
            DepartmentEntity departmentEntity = _mapper.Map<DepartmentEntity>(departmentDTO);
            int rowsAffected = await _appServerRepo.CreateDepartment(departmentEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<GetDepartmentDTO> GetDepartment(int id)
        {
            DepartmentEntity departmentEntity = await _appServerRepo.GetDepartmentById(id);
            if (!string.IsNullOrEmpty(departmentEntity.name))
            {
                GetDepartmentDTO departmentDTO = _mapper.Map<GetDepartmentDTO>(departmentEntity);
                return departmentDTO;
            }
            return new GetDepartmentDTO();
        }

        public async Task<bool> UpdateDepartment(UpdateDepartmentDTO departmentDTO)
        {
            DepartmentEntity departmentEntity = _mapper.Map<DepartmentEntity>(departmentDTO);
            int rowsAffected = await _appServerRepo.UpdateDepartment(departmentEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            int rowsAffected = await _appServerRepo.DeleteDepartment(id);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<List<GetDepartmentDTO>> GetDepartments()
        {
            List<DepartmentEntity> departmentEntities = await _appServerRepo.GetDepartments();
            List<GetDepartmentDTO> departmentDTOs = new List<GetDepartmentDTO>();
            foreach (DepartmentEntity departmentEntity in departmentEntities)
            {
                departmentDTOs.Add(_mapper.Map<GetDepartmentDTO>(departmentEntity));
            }
            return departmentDTOs;
        }

        #endregion department actions

        #region product type actions

        public async Task<bool> CreateProductType(CreateProductTypeDTO productTypeDTO)
        {
            ProductTypeEntity productTypeEntity = _mapper.Map<ProductTypeEntity>(productTypeDTO);
            int rowsAffected = await _appServerRepo.CreateProductType(productTypeEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> UpdateProductType(UpdateProductTypeDTO productTypeDTO)
        {
            ProductTypeEntity productTypeEntity = _mapper.Map<ProductTypeEntity>(productTypeDTO);
            int rowsAffected = await _appServerRepo.UpdateProductType(productTypeEntity);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<bool> DeleteProductType(int id)
        {
            int rowsAffected = await _appServerRepo.DeleteProductType(id);
            if (rowsAffected == 1)
                return true;
            return false;
        }

        public async Task<GetProductTypeDTO> GetProductType(int id)
        {
            ProductTypeEntity productTypeEntity = await _appServerRepo.GetProductTypeById(id);
            if (!string.IsNullOrEmpty(productTypeEntity.name))
            {
                GetProductTypeDTO productTypeDTO = _mapper.Map<GetProductTypeDTO>(productTypeEntity);
                return productTypeDTO;
            }
            return new GetProductTypeDTO();
        }

        public async Task<List<GetProductTypeDTO>> GetProductTypes()
        {
            List<ProductTypeEntity> productTypeEntities = await _appServerRepo.GetProductTypes();
            List<GetProductTypeDTO> productTypeDTOs = new List<GetProductTypeDTO>();
            foreach (ProductTypeEntity productTypeEntity in productTypeEntities)
            {
                productTypeDTOs.Add(_mapper.Map<GetProductTypeDTO>(productTypeEntity));
            }
            return productTypeDTOs;
        }

        #endregion product type actions
    }
}
