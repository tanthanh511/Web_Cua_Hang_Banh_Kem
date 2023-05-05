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

class OrderSerializer(serializers.ModelSerializer):
    class Meta:
        model = Order
        fields = ["id", "customer", "orderdate", "tranSactStatus", "deleted", "paid", "paymentid", "paymentdate", "note"]

class OrderDetailSerializer(serializers.ModelSerializer):
    class Meta:
        model = OrderDetail
        fields = ["id", "order", "products", "ordernumber", "quantity", "total"]