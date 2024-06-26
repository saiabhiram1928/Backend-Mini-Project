﻿using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Repositories
{
    public class PaymentRespository : CRUDRepository<int, Payment> , IPaymentRepository
    {
        public PaymentRespository(VideoStoreContext context) : base(context) { }

        public override async Task<IEnumerable<Payment>> GetAll()
        {
            return await _context.Payments.ToListAsync();   
        }

        public override async Task<Payment> GetById(int key)
        {
            var item = await _context.Payments.SingleOrDefaultAsync(x =>x.TransactionId == key);
            return item;
        }
        public async Task<Payment>  GetPaymentByOrderId(int orderId)
        {
            var item = await _context.Payments.SingleOrDefaultAsync(x => x.OrderId == orderId && x.PaymentSucess == true);
            return item;
        }
    }
}
