using Microsoft.EntityFrameworkCore;

namespace Homework_1.Models
{
    public class SeedData
    {
        //(1)撰寫靜態方法 Initialize(IServiceProvider serviceProvider)
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //(4)加上 using () 及 判斷資料庫是否有資料的程式
            using (MessageBoardContext context = new MessageBoardContext(serviceProvider.GetRequiredService<DbContextOptions<MessageBoardContext>>()))
            {

                //(4)加上 using () 及 判斷資料庫是否有資料的程式
                if (!context.MainContent.Any())
                {
                    //(2)資料表內的初始資料程式
                    string[] guid = { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };

                    context.MainContent.AddRange(
                        new MainContent
                        {
                            MainID = guid[0],
                            MTitle = "櫻桃鴨",
                            MContent = "這櫻桃鴨好好吃哦!!!下次一定要再來!!",
                            MPhoto = guid[0] + ".jpg",
                            MPhotoType = "美食",
                            NAuthor = "John",
                            CreatedDate = DateTime.Now
                        },
                        new MainContent
                        {
                            MainID = guid[1],
                            MTitle = "鴨油高麗菜",
                            MContent = "鴨油高麗菜好像稍微有點油....",
                            MPhoto = guid[1] + ".jpg",
                            MPhotoType = "美食",
                            NAuthor = "Mary",
                            CreatedDate = DateTime.Now
                        },
                        new MainContent
                        {
                            MainID = guid[2],
                            MTitle = "鴨油麻婆豆腐",
                            MContent = "這太下飯了！可以吃好幾碗白飯",
                            MPhoto = guid[2] + ".jpg",
                            MPhotoType = "美食",
                            NAuthor = "王小花",
                            CreatedDate = DateTime.Now
                        },
                        new MainContent
                        {
                            MainID = guid[3],
                            MTitle = "櫻桃鴨握壽司",
                            MContent = "櫻桃鴨與握壽司的完美絕配!! 口感搭配的真得恰到好處!! 下次來必點!!",
                            MPhoto = guid[3] + ".jpg",
                            MPhotoType = "美食",
                            NAuthor = "Winnie",
                            CreatedDate = DateTime.Now
                        },
                        new MainContent
                        {
                            MainID = guid[4],
                            MTitle = "三杯鴨",
                            MContent = "鴨肉鮮甜，飯桶的好朋友!",
                            MPhoto = guid[4] + ".jpg",
                            MPhotoType = "美食",
                            NAuthor = "Jack",
                            CreatedDate = DateTime.Now
                        }
                    );

                    context.SaveChanges();


                    context.Response.AddRange(

                        new Response
                        {
                            ResponseID = Guid.NewGuid().ToString(),
                            RContent = "我也覺得好吃！",
                            RAuthor = "小蘭",
                            CreatedDate = DateTime.Now,
                            MainID = guid[0]
                        },
                        new Response
                        {
                            ResponseID = Guid.NewGuid().ToString(),
                            RContent = "我不喜歡....",
                            RAuthor = "柯南",
                            CreatedDate = DateTime.Now,
                            MainID = guid[0]
                        },
                        new Response
                        {
                            ResponseID = Guid.NewGuid().ToString(),
                            RContent = "你最好餓死",
                            RAuthor = "小蘭",
                            CreatedDate = DateTime.Now,
                            MainID = guid[0]
                        },
                        new Response
                        {
                            ResponseID = Guid.NewGuid().ToString(),
                            RContent = "高麗菜這樣超好吃啊～",
                            RAuthor = "小英",
                            CreatedDate = DateTime.Now,
                            MainID = guid[1]
                        },
                        new Response
                        {
                            ResponseID = Guid.NewGuid().ToString(),
                            RContent = "口味似乎偏辣",
                            RAuthor = "阿狗",
                            CreatedDate = DateTime.Now,
                            MainID = guid[2]
                        },
                        new Response
                        {
                            ResponseID = Guid.NewGuid().ToString(),
                            RContent = "我還是喜歡生魚片的握壽司",
                            RAuthor = "嫩嫩",
                            CreatedDate = DateTime.Now,
                            MainID = guid[3]
                        },
                        new Response
                        {
                            ResponseID = Guid.NewGuid().ToString(),
                            RContent = "我也是喜歡生魚片的握壽司，但這個也不錯",
                            RAuthor = "王小花",
                            CreatedDate = DateTime.Now,
                            MainID = guid[3]
                        },
                        new Response
                        {
                            ResponseID = Guid.NewGuid().ToString(),
                            RContent = "三杯雞比較對味",
                            RAuthor = "芷若",
                            CreatedDate = DateTime.Now,
                            MainID = guid[4]
                        }

                        );
                    context.SaveChanges();


                    //(3)撰寫上傳圖片的程式
                    string SeedPhotosPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedPhotos");//取得來源照片路徑
                    string BookPhotosPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BookPhotos");//取得目的路徑


                    string[] files = Directory.GetFiles(SeedPhotosPath);  //取得指定路徑中的所有檔案

                    for (int i = 0; i < files.Length; i++)
                    {
                        string destFile = Path.Combine(BookPhotosPath, guid[i] + ".jpg");


                        File.Copy(files[i], destFile);
                    }
                }
            } //using結束
        }
    }
}
