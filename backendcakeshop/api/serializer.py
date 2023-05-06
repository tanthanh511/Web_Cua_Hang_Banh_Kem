from rest_framework import serializers
from cakeshop.models import * 

# xuất trường dữ liệu
class CategorySerializer(serializers.ModelSerializer):
    class Meta:
        model = Category
        fields = ["id", "name", "description", "ordering", "published", "thumb", "title", "alias", "metadesc", "metakey", "schemamarkup"]

class ProductSerializer(serializers.ModelSerializer):
    class Meta:
        model = Products
        fields = ["id", "name", "shortdesc", "description", "category", "price", "discount", "thumb", "datecreated", "datemodified", "bestseller", "homeflag", "active", "bestseller", "tags", "title", "alias", "metadesc", "metakey", "unitslnstock"]

class RoleSerializer(serializers.ModelSerializer):
    class Meta:
        model = Roles
        fields = ["id", "name", "description"]

class AccountSerializer(serializers.ModelSerializer):
    class Meta:
        model = Accounts
        fields = ["id", "name", "phone", "email", "password", "active", "createdate", "roles"]

class PageSerializer(serializers.ModelSerializer):
    class Meta:
        model = Pages
        fields = ["id", "name", "contents", "thumb", "published", "title", "alias", "createdate"]

class TinDangSerializer(serializers.ModelSerializer):
    class Meta:
        model = TinDang
        fields = ["id", "title", "shortcontent", "content", "thumb", "published", "alias", "createdate", "author", "tags", "account", "category", "ishot", "isnewfeed", "views"]

class TranSactStatusSerializer(serializers.ModelSerializer):
    class Meta:
        model = TranSactStatus
        fields = ["id", "status", "description"]


# class OrderSerializer(serializers.ModelSerializer):
#     class Meta:
#         model = Order
#         fields = ["id", "customer", "orderdate", "note"]

# class OrderDetailSerializer(serializers.ModelSerializer):
#     class Meta:
#         model = OrderDetail
#         fields = ["id","order", "products`", "quantity", "total"]




#  hàm lấy một số thông tin cần của product
class SimpleProductSerializer(serializers.ModelSerializer):
    class Meta:
        model = Products
        fields = ["id", "name", "price"]

# xem thông tin giỏ hàng
class OrderDetailSerializer(serializers.ModelSerializer):
    # product = SimpleProductSerializer(many=False)
    sub_total = serializers.SerializerMethodField(method_name="total")
    class Meta:
        model = OrderDetail
        fields = ["id", "order", "products", "quantity", "sub_total"]

    def total(self, orderdetail: OrderDetail):
        return orderdetail.quantity * orderdetail.products.price
    
class OrderSerializer(serializers.ModelSerializer):
    items = OrderDetailSerializer (many=True, read_only=True)
    grand_total = serializers.SerializerMethodField(method_name="main_total")
    class Meta:
        model = Order
        fields = ["id", "customer", "items", "orderdate", "grand_total"]
    def main_total(self, order: Order):
        items = order.items.all()
        total = sum([item.quantity * item.products.price for item in items]) 
        return total
     


    
# class UpdateOrderDetailSerializer(serializers.ModelSerializer):
#     class Meta: 
#         models = OrderDetail
#         fields =["quantity"]

# class AddOrderDetailSerializer(serializers.ModelSerializer):
   

#     def save(self, **kwargs):
#         order_id = self.context["id"]
#         product_id = self.validated_data["id"]
#         quantity = self.validated_data["quantity"]

#         try:
#             oderitems = OrderDetail.objects.get(product_id=product_id, oder_id=order_id)
#             oderitems.quantity += quantity
#             oderitems.save()

#             self.instance = oderitems
#         except:
#             self.instance = OrderDetail.objects.create(
#                 product_id=product_id,
#                 order_id=order_id,
#                 quantity=quantity,
#             )
#         return self.instance

#     class Meta:
#         model = OrderDetail
#         fields = ["id", "product", "quantity"]