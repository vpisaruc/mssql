import cProfile
from mimesis import Person, Numbers, Food, Datetime, Code
import random

person = Person('ru')
num = Numbers()
products = Food('ru')
card_code = Code()
date_time = Datetime()

array_transaction_type = ["Cash", "Card"]
array_product_prices = []
array_orders = []
array_orders_price = []
array_clients_id = []
array_transaction_id = []


file_clients = open('clients_table.txt', 'w')
file_products = open('products_table.txt', 'w')
file_bonuscards = open('bonuscards_table.txt', 'w')
file_orders = open('orders_table.txt', 'w')
file_transactions = open('transactions_table.txt', 'w')

def create_and_shuffle_array(size, array):
    for i in range(size):
        array.append(i)
    random.shuffle(array)

#client table
def generate_client_table(count):
    for i in range(count):
         str_clients = str(i+1) + "\t" + str(person.full_name()) + "\t" \
                       + str(person.telephone()) + "\t" + str(person.email()) + '\n'
         file_clients.write(str_clients)



#generate prices for products
def generate_prices(count):
    for i in range(count):
        array_product_prices.append(num.between(minimum=30, maximum=5000))



#products table
def generate_products_table(count):
    generate_prices(count)
    cnt = 0
    for i in range(int(count/4)):
         str_products = str(i+1) + "\t" + str(products.dish()) + "\t" + \
         str(array_product_prices[i]) + "\t" + str(random.randint(0,1)) + '\n'
         file_products.write(str_products)
         cnt += 1

    for i in range(int(count/4)):
        str_products = str(cnt + 1) + "\t" + str(products.drink()) + "\t" + str(array_product_prices[i + 100]) + \
        "\t" + str(random.randint(0,1)) + '\n'
        file_products.write(str_products)
        cnt += 1

    for i in range(int(count/4)):
         str_products = str(cnt + 1) + "\t" + str(products.fruit()) + "\t" + str(array_product_prices[i + 200]) + \
         "\t" + str(random.randint(0,1)) + '\n'
         file_products.write(str_products)
         cnt += 1

    for i in range(int(count/4)):
         str_products = str(cnt + 1) + "\t" + str(products.vegetable()) + "\t" + str(array_product_prices[i + 300]) + \
         "\t" + str(random.randint(0,1)) + '\n'
         file_products.write(str_products)
         cnt += 1

#Generate orders
def generate_orders_table(count):
    for __ in range(count):
        order_array = num.integers(start=1, end=400, length=random.randint(1, 15))
        array_orders.append(order_array)

    for i in range(len(array_orders)):
        sum = 0
        cnt = 0
        for j in range(len(array_orders[i])):
            str_orders = str(i + 1) + '\t' + str(array_orders[i][j]) + '\n'
            file_orders.write(str_orders)
            sum += array_product_prices[array_orders[i][j] - 1]
            cnt += 1

            #вычисляем общую сумму заказа
            if(cnt == len(array_orders[i])):
                array_orders_price.append(sum)


#Transaction table
def generate_transactions(count):
     for i in range(count):
          str_transaction = str(i + 1) + "\t" + str(random.randint(1,100)) + "\t" + \
          str(date_time.year(minimum=1990, maximum=2018)) + "-"+ \
                str(num.between(minimum=1, maximum=12)) + "-" + str(num.between(minimum=1, maximum=28)) + "\t" + \
                str(date_time.time()) + "\t" + str(array_orders_price[i]) + "\t" + \
                str(array_transaction_type[random.randint(0,1)]) + "\t" + str(random.randint(1,5)) + '\n'

          file_transactions.write(str_transaction)



#Bonus card table
def generate_bonuscards(count):
    create_and_shuffle_array(count, array_clients_id)
    create_and_shuffle_array(count, array_transaction_id)
    for i in range(count):
         str_bonuscard = str(i + 1) + '\t' + str(array_transaction_id[i] + 1) + '\t' + str(array_clients_id[i] + 1) + \
                         '\t' + str(card_code.imei()) + \
                         '\t' + str(date_time.year(minimum=1990, maximum=2018)) + "-"+ \
                          str(num.between(minimum=1, maximum=12)) + "-" + str(num.between(minimum=1, maximum=28)) + \
                         '\t' + str(random.randint(0, 1000)) + '\n'

         file_bonuscards.write(str_bonuscard)



def main():
    countClient = int(input('Введите колличество клиентов: '))
    print('\n')
    countProduct = int(input('Введите колличество товаров: '))
    print('\n')
    countTransaction = int(input('Введите колличество транзакций: '))
    print('\n')
    countBonuscard = int(input('Введите колличество банускарт: '))
    print('\n')

    generate_client_table(countClient)
    generate_products_table(countProduct)
    generate_orders_table(countTransaction)
    generate_transactions(countTransaction)
    generate_bonuscards(countBonuscard)

    file_bonuscards.close()
    file_transactions.close()
    file_orders.close()
    file_products.close()
    file_clients.close()

if __name__ == "__main__":
    main()