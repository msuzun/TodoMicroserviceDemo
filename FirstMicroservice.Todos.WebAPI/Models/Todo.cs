﻿namespace FirstMicroservice.Todos.WebAPI.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Work { get; set; } = default!;
    }
}
