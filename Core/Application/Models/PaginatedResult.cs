using System;

namespace Application.Models;

public class PaginatedResult<T>
{
    public IEnumerable<T> Result { get; set; } = new List<T>();
    public int Page { get; set; } // หมายเลขหน้าปัจจุบัน
    public int PageSize { get; set; } // ขนาดของแต่ละหน้า (จำนวนรายการต่อหน้า)
    public int TotalCount { get; set; } // จำนวนรายการทั้งหมดในฐานข้อมูล
}