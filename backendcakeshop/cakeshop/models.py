from django.db import models

# sản phẩm
class Category(models.Model):
    name = models.CharField(max_length=100)
    description = models.TextField()
    ordering = models.IntegerField()
    published = models.BooleanField(default=True)
    thumb = models.TextField()
    title = models.TextField()
    alias = models.TextField()
    metadesc = models.TextField()
    metakey = models.TextField()
    schemamarkup = models.TextField()
      # lấy tên 
    def __str__(self) -> str:
            return self.name


class Products(models.Model):
    name = models.CharField(max_length=100)
    shortdesc = models.TextField()
    description = models.TextField()
    category = models.ForeignKey(Category, on_delete=models.SET_NULL, null=True)
    price = models.FloatField(default=100.00)
    discount = models.IntegerField()
    thumb = models.TextField()
    datecreated = models.DateTimeField(auto_now_add=True)
    datemodified = models.DateTimeField(auto_now= True)
    bestseller = models.BooleanField(default= True)
    homeflag = models.BooleanField(default=True)
    active = models.BooleanField(default=True)
    tags = models.CharField(max_length=100)
    title= models.TextField()
    alias = models.TextField()
    metadesc = models.TextField()
    metakey = models.TextField()
    unitslnstock = models.IntegerField()
    # lấy tên 
    def __str__(self) -> str:
            return self.name

# thuộc tính giá
class Attributes(models.Model):
    name = models.CharField(max_length=100)
    def __str__(self) -> str:
        return self.name
    
class AttributesPrices(models.Model):
    attributes = models.ForeignKey(Attributes, on_delete= models.CASCADE)
    products = models.ForeignKey(Products, on_delete= models.CASCADE)
    price = models.FloatField(default=100.00)
    active = models.BooleanField(default=True)
    def __str__(self) -> str:
        return self.name

# tài khoản user
class Roles(models.Model):
     name = models.CharField(max_length=100)
     description = models.TextField()
     def __str__(self) -> str:
        return self.name
     
class Accounts(models.Model):
     name = models.CharField(max_length=100)
     phone = models.CharField(max_length=10)
     email = models.CharField(max_length=50)
     password= models.CharField(max_length=50)
     active = models.BooleanField(default=True)
     createdate = models.DateTimeField(auto_now_add=True)
     roles = models.ForeignKey(Roles, on_delete=models.SET_NULL, null=True)
     def __str__(self) -> str:
        return self.name


class TinDang(models.Model):
     title = models.TextField()
     shortcontent = models.TextField()
     content = models.TextField()
     thumb = models.TextField()
     published = models.BooleanField(default=True)
     alias = models.TextField()
     createdate = models.DateTimeField(auto_now_add=True)
     author = models.CharField(max_length=100)
     tags = models.CharField(max_length=100)
     account = models.ForeignKey(Accounts, on_delete=models.SET_NULL, null=True)
     category = models.ForeignKey(Category,on_delete=models.SET_NULL, null=True)
     ishot = models.BooleanField(default=True)
     isnewfeed = models.BooleanField(default=True)
     views = models.IntegerField()
     def __str__(self) -> str:
        return self.title
     
# khách hàng
class Customer(models.Model):
     name = models.CharField(max_length=100)
     avata = models.TextField()
     address = models.TextField()
     email = models.CharField(max_length=100)
     phone = models.CharField(max_length=10)
     createdate = models.DateTimeField(auto_now_add= True)
     password = models.CharField(max_length=100)
     active = models.BooleanField(default=True)
     def __str__(self) -> str:
        return self.name
     
# tình trạng đơn hàng 
class TranSactStatus(models.Model):
     status = models.CharField(max_length=50)
     description = models.TextField()
     def __str__(self) -> str:
        return self.status

# mua hàng
class Order(models.Model):
     customer = models.ForeignKey(Customer, on_delete= models.CASCADE)
     orderdate = models.DateTimeField(auto_now_add=True)
     tranSactStatus = models.ForeignKey(TranSactStatus, on_delete=models.SET_NULL, null=True)
     deleted = models.BooleanField(default=False)
     paid = models.BooleanField(default=False)
     paymentid = models.IntegerField(null=True)
     paymentdate = models.DateField(null=True)
     grand_total =  models.FloatField(default=100.00)
     note = models.TextField()
     def __str__(self) -> str:
        return self.customer.name

class OrderDetail(models.Model):
      order = models.ForeignKey(Order, on_delete= models.CASCADE, related_name="items", null=True, blank=True,)
      products = models.ForeignKey(Products, on_delete= models.CASCADE, blank=True, null=True, related_name="oderitems",)
      quantity = models.IntegerField()
      sub_total = models.FloatField(default=100.00)
      def __str__(self) -> str:
        return self.products.name

# page hương dan, gioi thieu
class Pages(models.Model):
     name = models.CharField(max_length=100)
     contents = models.TextField()
     thumb = models.TextField()
     published = models.BooleanField(default=True)
     title = models.TextField()
     alias = models.TextField()
     createdate = models.DateTimeField(auto_now_add=True)
     def __str__(self) -> str:
        return self.name


     
   


