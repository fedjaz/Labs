package com.fedjaz.keeps

import android.os.Bundle
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.GridLayoutManager
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.fedjaz.keeps.dummy.DummyContent

/**
 * A fragment representing a list of Items.
 */
class KeepFragment : Fragment() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.keep_fragment_item_list, container, false)

        // Set the adapter
        if (view is RecyclerView) {
            with(view) {
                layoutManager = GridLayoutManager(context, 2)
                adapter = KeepRecyclerViewAdapter(DummyContent.ITEMS)
            }
        }
        return view
    }

    companion object {
        @JvmStatic
        fun newInstance() =
            KeepFragment().apply {
                arguments = Bundle().apply {

                }
            }
    }
}