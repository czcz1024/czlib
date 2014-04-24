namespace CZLib.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 缓存操作类
    /// </summary>
    public class CacheOperator
    {
        private ICache cache;

        private ILev2Cache lev2;

        public CacheOperator():this(new DefaultCache())
        {
        }

        public CacheOperator(ICache cache)
            : this(cache, new ThreadLev2Cache())
        {
            
        }

        public CacheOperator(ICache cache, ILev2Cache lev2)
        {
            this.cache = cache;
            this.lev2 = lev2;
        }

        private object GetFromLev2(string key)
        {
            if (this.lev2 != null)
            {
                var obj = this.lev2.Get(key);
                return obj;
            }
            return null;
        }

        private void SetToLev2(string key, object obj)
        {
            if (this.lev2 != null)
            {
                this.lev2.Set(key, obj);
            }
        }

        public virtual object Get(string key)
        {
            var cc = this.GetFromLev2("cache." + key);
            if (cc != null) return cc;
            var co= this.cache.Get(key);
            this.SetToLev2("cache."+key, co);
            return co;
        }

        public virtual T Get<T>(string key)
        {
            var v = this.Get(key);
            if (v is T) return (T)v;
            return default(T);
        }

        public virtual T Get<T>(string key, Func<T> getter, DateTime expire)
        {
            var v = this.Get(key);
            if (v is T)
            {
                return (T)v;
            }

            if (getter == null) return default(T);
            var t = getter();
            this.Set(key, t, expire);
            return t;
        }

        public virtual T Get<T>(string key, Func<T> getter)
        {
            return this.Get<T>(key, getter, DateTime.Now.AddMinutes(5));
        }


        public virtual bool Set(string key, object obj, DateTime expire)
        {
            this.SetToLev2("cache." + key, obj);
            return this.cache.Set(key, obj, expire);
        }

        public virtual bool Set(string key, object obj)
        {
            return this.Set(key, obj, DateTime.Now.AddMinutes(5));
        }

        public virtual bool Delete(string key)
        {
            this.SetToLev2("cache." + key, null);
            return this.cache.Delete(key);
        }

        /// <summary>
        /// 通过一个Key返回
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <typeparam name="TKey">实体键类型</typeparam>
        /// <param name="listCacheKey">这个实体集合对应的KEY</param>
        /// <param name="getListObj">获取所有实体集合</param>
        /// <param name="getSingleModel">通过一个ID找到一个实体</param>
        /// <param name="getModelKey">通过一个实体找到一个ID</param>
        /// <param name="singleModelCacheKey">通过id，生成实体缓存key</param>
        /// <param name="listexpire">listint的过期时间</param>
        /// <param name="modelexpire">Model的过期时间</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetListCache<T, TKey>(string listCacheKey, Func<IEnumerable<T>> getListObj, Func<TKey, T> getSingleModel, Func<T, TKey> getModelKey, Func<TKey, string> singleModelCacheKey, DateTime listexpire, DateTime modelexpire) where T : class
        {
            var list = this.Get<List<TKey>>(listCacheKey);//找到id list缓存
            if (list == null)
            {
                var all = getListObj().ToList();//直接读库获取obj list
                this.Set(listCacheKey, all.Select(getModelKey).ToList(), listexpire);//写id list缓存
                foreach (var item in all)
                {
                    this.Set(singleModelCacheKey(getModelKey(item)), item, modelexpire);//遍历写obj 缓存
                }
                return all;
            }
            else
            {
                var result = list.Select(x =>
                    {
                        var singleKey = singleModelCacheKey(x);//生成obj 缓存key
                        var model = this.Get<T>(singleKey);//从缓存里找obj
                        if (model == null)
                        {
                            model = getSingleModel(x);//没有读库
                            this.Set(singleKey, model, modelexpire);//写到缓存
                        }
                        return model;
                    });
                return result;
            }

            //var v = Get(listCacheKey);
            //List<T> list = new List<T>();
            //if (v != null)
            //{
            //    var listint = v as List<TKey>;
            //    foreach (var obj in listint)
            //    {
            //        var objt = Get(singleModelCacheKey(obj));
            //        if (objt != null)
            //        {
            //            list.Add((T)objt);
            //        }
            //        else
            //        {
            //            var objv = getSingleModel(obj);
            //            Set(singleModelCacheKey(obj), objv, modelexpire);
            //            list.Add(objv);
            //        }
            //    }
            //    return list;
            //}
            //else
            //{
            //    List<TKey> objlistint = new List<TKey>();
            //    foreach (var s in getListObj())
            //    {
            //        objlistint.Add(getModelKey(s));
            //    }
            //    Set(listCacheKey, objlistint, listexpire);
            //    var listint = v as List<TKey>;
            //    foreach (var obj in listint)
            //    {
            //        var objt = Get(singleModelCacheKey(obj));
            //        if (objt != null)
            //        {
            //            list.Add((T)objt);
            //        }
            //        else
            //        {
            //            var objv = getSingleModel(obj);
            //            Set(singleModelCacheKey(obj), objv, modelexpire);
            //            list.Add(objv);
            //        }
            //    }
            //    return list;
            //}
        }
    }
}