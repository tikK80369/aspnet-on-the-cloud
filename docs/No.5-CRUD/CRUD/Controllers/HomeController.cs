using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly MvcCRUDContext _context;

        public HomeController(MvcCRUDContext context)
        {
            _context = context;
        }

        // 一覧ページ表示時
        public async Task<IActionResult> Index()
        {
            //データの一覧を取得
            return View(await _context.CRUDTest.ToListAsync());
        }

        // 詳細ページ表示時
        public async Task<IActionResult> Details(int? id)
        {
            //id=nullの場合のエラー処理
            if (id == null)
            {
                return NotFound();
            }

            //idが一致するレコードを取得
            var uSERLIST = await _context.CRUDTest.FirstOrDefaultAsync(m => m.Id == id);

            //検索結果nullの場合のエラー処理
            if (uSERLIST == null)
            {
                return NotFound();
            }

            return View(uSERLIST);
        }

        // 新規作成ページ表示時
        public IActionResult Create()
        {
            return View();
        }

        // 新規作成時アクション
        [HttpPost]
        [ValidateAntiForgeryToken] //CSRF防止用のtokenのチェック
        public async Task<IActionResult> Create([Bind("Id,Userid,Name,Age,D_CRT")] USERLIST uSERLIST)
        {
            //POSTされた時、値を受け取ってDBに登録
            if (ModelState.IsValid)
            {
                //登録処理
                _context.Add(uSERLIST);
                await _context.SaveChangesAsync();
                //登録成功時、一覧を表示
                return RedirectToAction(nameof(Index));
            }
            //問題があった場合は元ページを表示
            return View(uSERLIST);
        }


        // 更新ページ表示時
        public async Task<IActionResult> Edit(int? id)
        {
            //id=nullの場合のエラー処理
            if (id == null)
            {
                return NotFound();
            }

            //idが一致するレコードを取得
            var uSERLIST = await _context.CRUDTest.FindAsync(id);

            //検索結果nullの場合のエラー処理
            if (uSERLIST == null)
            {
                return NotFound();
            }
            return View(uSERLIST);
        }

        // 更新時アクション
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Userid,Name,Age,D_CRT")] USERLIST uSERLIST)
        {
            //POSTされた時、値を受け取ってDBに登録
            if (ModelState.IsValid)
            {
                //更新処理
                _context.Entry(uSERLIST).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                //登録成功時、一覧を表示
                return RedirectToAction(nameof(Index));
            }
            //問題があった場合は元ページを表示
            return View(uSERLIST);
        }
    
        // 削除ページ表示時
        public async Task<IActionResult> Delete(int? id)
        {
            //id=nullの場合のエラー処理
            if (id == null)
            {
                return NotFound();
            }

            //idが一致するレコードを取得
            var uSERLIST = await _context.CRUDTest.FindAsync(id);

            //検索結果nullの場合のエラー処理
            if (uSERLIST == null)
            {
                return NotFound();
            }
            return View(uSERLIST);
        }

        //削除時アクション
        //同じ名前、同じ引数が定義できない（実際は?がついているからできるけど）
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //削除対象を取得・削除・保存
            var uSERLIST = await _context.CRUDTest.FindAsync(id);
            _context.CRUDTest.Remove(uSERLIST);
            await _context.SaveChangesAsync();
            //削除後、一覧を表示
            return RedirectToAction(nameof(Index));
        }

    }
}
