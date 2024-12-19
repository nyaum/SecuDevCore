using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SecuDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecuDev.Helper
{
    public static class SessionHelper 
    {
        private static IHttpContextAccessor _httpContextAccessor;

        // IHttpContextAccessor를 설정하는 메서드
        public static void SetHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // 정적 메서드에서 세션 값 가져오기
        public static string GetUserName()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("UserName");
        }

    }
}