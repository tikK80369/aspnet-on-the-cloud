using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            //データ取得や更新は非同期で処理する
            return View(await _context.CRUDTest.ToListAsync());
        }

        // 詳細ページ表示時
        public async Task<IActionResult> Details(int? id)
        {
            //ページ遷移時に渡されるid=nullの場合のエラー処理
            if (id == null)
            {
                return NotFound();
            }

            //idが一致するレコードを取得
            var userListModel = await _context.CRUDTest.FirstOrDefaultAsync(m => m.Id == id);

            //検索結果nullの場合のエラー処理
            if (userListModel == null)
            {
                return NotFound();
            }

            return View(userListModel);
        }

        // 新規作成ページ表示時
        public IActionResult Create()
        {
            return View();
        }

        // 新規作成時アクション
        [HttpPost]
        [ValidateAntiForgeryToken] //CSRF防止用のtokenのチェック
        public async Task<IActionResult> Create([Bind("Id,Userid,Name,Age,D_CRT")] UserListModel userListModel)
        {
            //POSTされた時、値を受け取ってDBに登録
            if (ModelState.IsValid)
            {
                //登録処理
                _context.Add(userListModel);
                await _context.SaveChangesAsync();
                //登録成功時、一覧を表示
                return RedirectToAction(nameof(Index));
            }
            //問題があった場合は元ページを表示
            return View(userListModel);
        }


        // 更新ページ表示時
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userListModel = await _context.CRUDTest.FirstOrDefaultAsync(m => m.Id == id);

            if (userListModel == null)
            {
                return NotFound();
            }
            return View(userListModel);
        }

        // 更新時アクション
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Userid,Name,Age,D_CRT")] UserListModel userListModel)
        {
            if (ModelState.IsValid)
            {
                //更新処理
                _context.Entry(userListModel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userListModel);
        }

        // 削除ページ表示時
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userListModel = await _context.CRUDTest.FirstOrDefaultAsync(m => m.Id == id);

            if (userListModel == null)
            {
                return NotFound();
            }
            return View(userListModel);
        }

        //削除時アクション
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //削除対象を取得・削除・保存
            var userListModel = await _context.CRUDTest.FindAsync(id);
            _context.CRUDTest.Remove(userListModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}